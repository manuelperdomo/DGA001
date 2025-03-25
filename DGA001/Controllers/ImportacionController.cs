using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using DGA001.Services;
using DGA001.Models;
namespace DGA001.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportacionController : ControllerBase
    {
        private readonly ImportacionContext _context;
        private readonly string _importFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Recibidos", "Importaciones");

        public ImportacionController(ImportacionContext context)
        {
            _context = context;
        }

        [HttpPost("subir")]
        public async Task<IActionResult> SubirArchivo(IFormFile archivo)
        {
            try
            {
                if (archivo == null || archivo.Length == 0)
                    return BadRequest("El archivo no fue enviado o está vacío.");

                // Crear la carpeta si no existe
                if (!Directory.Exists(_importFolderPath))
                    Directory.CreateDirectory(_importFolderPath);

                // Guardar el archivo
                string archivoPath = Path.Combine(_importFolderPath, archivo.FileName);
                using (var stream = new FileStream(archivoPath, FileMode.Create))
                {
                    await archivo.CopyToAsync(stream);
                }

                // Procesar el archivo
                return await ProcesarArchivo(archivoPath);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al subir el archivo: {ex.Message}");
            }
        }

        private async Task<IActionResult> ProcesarArchivo(string archivoPath)
        {
            try
            {
                // Procesar el archivo
                var accessService = new AccessService(archivoPath);
                var importacionesData = accessService.LeerTabla("Importaciones");

                // Insertar los datos en la base de datos
                foreach (var importacion in importacionesData)
                {
                    _context.Importacions.Add(importacion);
                }

                await _context.SaveChangesAsync();

                // Eliminar el archivo después de procesarlo
                if (System.IO.File.Exists(archivoPath))
                    System.IO.File.Delete(archivoPath);

                return Ok("Importación completada.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al procesar el archivo: {ex.Message}");
            }
        }

    }
}
