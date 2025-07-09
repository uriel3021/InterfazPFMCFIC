using System.Text.Json;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

public class PlantillaService
{
    private readonly string _rutaBasePlantillas;

    public PlantillaService(IConfiguration configuration)
    {
        _rutaBasePlantillas = configuration["Plantillas:RutaBase"];
    }

    public byte[] LlenarPlantilla(string nombrePlantilla, string jsonValores)
    {
        Console.WriteLine("=== DEBUG PLANTILLA ===");
        Console.WriteLine($"Nombre plantilla: {nombrePlantilla}");
        Console.WriteLine($"Ruta base: {_rutaBasePlantillas}");
        
        if (string.IsNullOrEmpty(_rutaBasePlantillas))
        {
            throw new InvalidOperationException("La ruta base de las plantillas no está configurada.");
        }

        if (string.IsNullOrEmpty(nombrePlantilla))
        {
            throw new ArgumentException("El nombre de la plantilla no puede ser nulo o vacío.", nameof(nombrePlantilla));
        }

        // Ruta completa de la plantilla
        string rutaPlantilla = Path.Combine(_rutaBasePlantillas, nombrePlantilla);
        Console.WriteLine($"Ruta completa: {rutaPlantilla}");

        if (!File.Exists(rutaPlantilla))
        {
            Console.WriteLine($"ERROR: El archivo no existe en: {rutaPlantilla}");
            throw new FileNotFoundException("La plantilla no existe.", rutaPlantilla);
        }

        // Leer el JSON y convertirlo a un diccionario
        var valores = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonValores);
        Console.WriteLine("Valores a reemplazar:");
        foreach (var kvp in valores)
        {
            Console.WriteLine($"  {kvp.Key} = {kvp.Value}");
        }

        // Crear un archivo temporal para la plantilla generada
        string archivoTemporal = Path.GetTempFileName();
        File.Copy(rutaPlantilla, archivoTemporal, true);

        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(archivoTemporal, true, new OpenSettings { AutoSave = true }))
        {
            try
            {
                var body = wordDoc.MainDocumentPart.Document.Body;

                foreach (var marcador in valores)
                {
                    Console.WriteLine($"Procesando marcador: {marcador.Key}");
                    ReemplazarMarcador(body, marcador.Key, marcador.Value);
                }

                wordDoc.MainDocumentPart.Document.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR al procesar: {ex.Message}");
                throw new InvalidOperationException("Error al procesar la plantilla. Verifica que no contenga elementos no compatibles.", ex);
            }
        }

        // Leer el archivo generado como un arreglo de bytes
        byte[] archivoGenerado = File.ReadAllBytes(archivoTemporal);

        // Eliminar el archivo temporal
        File.Delete(archivoTemporal);

        Console.WriteLine("Plantilla procesada exitosamente");
        return archivoGenerado;
    }

    private void ReemplazarMarcador(Body body, string marcador, string valor)
    {
        var paragraphs = body.Descendants<Paragraph>();
        int reemplazos = 0;

        foreach (var paragraph in paragraphs)
        {
            var runs = paragraph.Descendants<Run>().ToList();

            // Combinar el texto de todos los Run en el párrafo
            string textoCompleto = string.Join(string.Empty, runs.Select(run => run.GetFirstChild<Text>()?.Text ?? string.Empty));

            // Verificar si el marcador está presente en el texto combinado
            string marcadorCompleto = "{{" + marcador + "}}";
            if (textoCompleto.Contains(marcadorCompleto))
            {
                Console.WriteLine($"  ENCONTRADO marcador {marcadorCompleto} en: {textoCompleto}");
                
                // Reemplazar el marcador con el valor, eliminando las llaves
                textoCompleto = textoCompleto.Replace(marcadorCompleto, valor);

                // Eliminar los Run existentes
                foreach (var run in runs)
                {
                    run.Remove();
                }

                // Crear un nuevo Run con el texto actualizado y estilo normal
                var nuevoRun = new Run(new Text(textoCompleto));
                paragraph.AppendChild(nuevoRun);
                
                reemplazos++;
            }
        }
        
        if (reemplazos == 0)
        {
            Console.WriteLine($"  NO ENCONTRADO el marcador {marcador} en el documento");
        }
        else
        {
            Console.WriteLine($"  REALIZADO {reemplazos} reemplazos para {marcador}");
        }
    }
}