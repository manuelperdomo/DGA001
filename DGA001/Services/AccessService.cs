using DGA001.Extensions;
using DGA001.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
namespace DGA001.Services
{
    public class AccessService
    {
        private readonly string _connectionString;

        public AccessService(string filePath)
        {
            _connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Persist Security Info=False;";
        }

        public List<Importacion> LeerTabla(string nombreTabla)
        {
            List<Importacion> importaciones = new List<Importacion>();

            try
            {
                using (OleDbConnection connection = new OleDbConnection(_connectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM {nombreTabla}";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            using (var context = new ImportacionContext()) // Asegúrate de usar tu DbContext real
                            {
                                // Buscar si el importador ya existe
                                var rnc = reader["RNC"].ToString();
                                var importador = context.Importadors.FirstOrDefault(i => i.Rnc == rnc);
                                if (importador == null)
                                {
                                    importador = new Importador
                                    {
                                        Nombre = reader.GetStringSafe("NOMBRE_IMP"),
                                        TipoDocumento = reader.GetStringSafe("TIPO_DOC"),
                                        Rnc = reader.GetStringSafe("RNC"),
                                        Regimen = reader.GetStringSafe("REGIMEN")
                                    };
                                    context.Importadors.Add(importador);
                                    context.SaveChanges();
                                }

                                // Insertar Declaración
                                var declaracion = new Declaracion
                                {
                                    FechaDeclara = reader.GetDateOnlySafe("F_DECLARA") ?? DateOnly.MinValue,
                                    FechaDesem = reader.GetDateOnlySafe("F_DESEM") ?? DateOnly.MinValue,
                                    FechaLiquida = reader.GetDateOnlySafe("F_LIQUIDA") ?? DateOnly.MinValue,

                                    Anio = reader.GetIntSafe("ANO"),
                                    TipoDespacho = reader.GetStringSafe("TIPO_DESPACHO"),
                                    Colect = reader.GetStringSafe("COLECT"),
                                    Colecturia = reader.GetStringSafe("COLECTURIA"),
                                    Declara = reader.GetStringSafe("DECLARA"),
                                    Manifiesto = reader.GetStringSafe("MANIFIESTO"),
                                    ConEmb = reader.GetStringSafe("CON_EMB"),
                                    ImportadorId = importador.Id
                                };
                                context.Declaracions.Add(declaracion);
                                context.SaveChanges(); // Guarda para obtener el Id generado

                                // Insertar Transporte
                                var transporte = new Transporte
                                {
                                    DeclaracionId = declaracion.Id,
                                    CodigoPuerto = reader.GetStringSafe("CODIGO_PUE"),

                                    Puerto = reader.GetStringSafe("PUERTO"),
                                    PaisProcesoIso = reader.GetStringSafe("P_PROC_ISO"),
                                    PaisProceso = reader.GetStringSafe("PAIS_PROC"),
                                    PaisOrigenIso = reader.GetStringSafe("P_ORIG_ISO"),
                                    PaisOrigen = reader.GetStringSafe("PAIS_ORIG"),
                                    Embarcador = reader.GetStringSafe("EMBARCADOR")
                                };
                                context.Transportes.Add(transporte);
                                context.SaveChanges(); // Guarda para obtener el Id generado

                                // Insertar Importación
                                var importacion = new Importacion
                                {
                                    NumeroDeclaracion = reader.GetStringSafe("DECLARA"),
                                    FechaDeclara = reader.GetDateOnlySafe("F_DECLARA") ?? DateOnly.MinValue,
                                    TipoDespacho = reader.GetStringSafe("TIPO_DESPACHO"),
                                    ImportadorId = importador.Id,
                                    TransporteId = transporte.Id
                                };
                                context.Importacions.Add(importacion);
                                context.SaveChanges();

                                // Insertar Vehículo si aplica
                                if (!string.IsNullOrEmpty(reader["MARCA"].ToString()))
                                {
                                    var vehiculo = new Vehiculo
                                    {
                                        DeclaracionId = declaracion.Id,
                                        Marca = reader.GetStringSafe("MARCA"),
                                        Modelo =    reader.GetStringSafe("MODELO"),
                                        AnioFabricacion = reader.GetIntSafe("ANO_FAB"),
                                        TipoVehiculo = reader.GetStringSafe("TIPO_VEH"),
                                        Motor = reader.GetStringSafe("MOTOR_VEH"),
                                        Color = reader.GetStringSafe("COLOR_VEH"),
                                        Chasis = reader.GetStringSafe("CHAS_VEH"),
                                        CilindrajeCc = reader.GetStringSafe("CC_VEH"),
                                        Estado = reader.GetStringSafe("ESTADO")
                                    };
                                    context.Vehiculos.Add(vehiculo);
                                    context.SaveChanges();
                                }

                                // Insertar Impuestos
                                var impuestos = new Impuesto
                                {
                                    PrecioAlPorMenor = reader.GetDecimalSafe("PRECIO_ALPORMENOR"),
                                    FobUnit = reader.GetDecimalSafe("FOB_UNIT"),
                                    ValorFob = reader.GetDecimalSafe("V_FOB"),
                                    Flete = reader.GetDecimalSafe("FLETE"),
                                    Seguro = reader.GetDecimalSafe("SEGURO"),
                                    Otros = reader.GetDecimalSafe("OTROS"),
                                    Vcifbruto = reader.GetDecimalSafe("V_CIF_BRUT"),
                                    Vcifneto = reader.GetDecimalSafe("V_CIF_NETO"),
                                    Gravamen = reader.GetDecimalSafe("GRAVAMEN"),
                                    Selectivo = reader.GetDecimalSafe("SELECTIVO"),
                                    Itbis = reader.GetDecimalSafe("ITBIS"),
                                    //TotalApagar = reader.GetDecimalSafe("T_A_PAGAR")
                                };
                                context.Impuestos.Add(impuestos);
                                context.SaveChanges();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer el archivo Access: " + ex.Message);
            }

            return importaciones;
        }
    
    }
}
