using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace Vertex.App.Services
{
    public class AutoUpdaterService
    {
        private const string UpdateInfoUrl = "https://tuservidor.com/vertex/update.json";
        private const string TempInstallerName = "VertexInstaller_temp.exe";

        public async Task CheckAndUpdateAsync()
        {
            try
            {
                string tempPath = Path.Combine(Path.GetTempPath(), TempInstallerName);

                using var client = new HttpClient();
                var json = await client.GetStringAsync(UpdateInfoUrl);
                dynamic info = JObject.Parse(json);
                string latestVersion = info.version;
                string installerUrl = info.installerUrl;

                string currentVersion = "1.0.0"; // Obtener dinámicamente si se desea
                if (latestVersion == currentVersion) return;

                var result = MessageBox.Show(
                    $"Se detectó la versión {latestVersion}. La aplicación necesita cerrarse para actualizar. ¿Deseas continuar?",
                    "Actualización disponible",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information);

                if (result != MessageBoxResult.Yes) return;

                // Descarga con manejo de errores e intentos
                int maxRetries = 3;
                for (int attempt = 1; attempt <= maxRetries; attempt++)
                {
                    try
                    {
                        var installerBytes = await client.GetByteArrayAsync(installerUrl);
                        await File.WriteAllBytesAsync(tempPath, installerBytes);
                        break;
                    }
                    catch
                    {
                        if (attempt == maxRetries)
                        {
                            MessageBox.Show(
                                "No se pudo descargar la actualización tras varios intentos.",
                                "Error de actualización",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                            return;
                        }
                        await Task.Delay(2000); // espera antes de reintentar
                    }
                }

                // Ejecutar instalador silencioso
                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = tempPath,
                    Arguments = "/VERYSILENT /SUPPRESSMSGBOXES /NORESTART",
                    UseShellExecute = true
                });
                process.WaitForExit();

                MessageBox.Show(
                    "La actualización se aplicó correctamente.",
                    "Actualización completada",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                Process.Start(new ProcessStartInfo
                {
                    FileName = System.Reflection.Assembly.GetExecutingAssembly().Location,
                    UseShellExecute = true
                });

                Application.Current.Shutdown();
            }
            catch (HttpRequestException)
            {
                MessageBox.Show(
                    "No se pudo conectar al servidor de actualización.",
                    "Error de actualización",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ocurrió un error al intentar actualizar: {ex.Message}",
                    "Error de actualización",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
