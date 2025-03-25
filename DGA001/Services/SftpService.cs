using WinSCP;
using System;

namespace DGA001.Services
{

    public class SftpService
    {
        public void DescargarArchivos()
        {
            try
            {
                using (Session session = new Session())
                {
                    // Configurar conexión
                    WinSCP.SessionOptions sessionOptions = new WinSCP.SessionOptions
                    {
                        Protocol = Protocol.Sftp,
                        HostName = "10.0.0.59",
                        UserName = "usuario_sftp",
                        Password = "1234",
                        SshHostKeyFingerprint = "mnHXnMnu01LNyjR0u1QuMSVgcjH36A8iCbWvTj/Svno"
                    };


                    session.Open(sessionOptions);

                    // Ruta donde se guardarán los archivos descargados en el Servidor 1
                    string destino = @"C:\ArchivosAccess\";
                    if (!System.IO.Directory.Exists(destino))
                    {
                        System.IO.Directory.CreateDirectory(destino);
                    }

                    // Descargar archivos de Exportaciones
                    session.GetFiles("/home/manuel/Archivos/Exportaciones/*.accdb", destino, false).Check();

                    // Descargar archivos de Importaciones
                    session.GetFiles("/home/manuel/Archivos/Importaciones/*.accdb", destino, false).Check();
                }

                Console.WriteLine("Descarga completada.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error en la descarga: " + e.Message);
            }
        }
    }

}

