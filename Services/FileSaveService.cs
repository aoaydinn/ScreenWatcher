using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace ScreenWatcher.Services
{
    public class FileSaveService
    {
        public string Save(Bitmap bitmap, string targetFolder, string format, string fileNamePrefix = "Screenshot")
        {
            if (bitmap == null) return null;

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string safePrefix = string.IsNullOrWhiteSpace(fileNamePrefix) ? "Screenshot" : fileNamePrefix;

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                safePrefix = safePrefix.Replace(c, '_');
            }

            try
            {
                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }

                if (string.Equals(format, "PDF", StringComparison.OrdinalIgnoreCase))
                {
                    string filename = Path.Combine(targetFolder, $"{safePrefix}_{timestamp}.pdf");
                    using (var document = new PdfDocument())
                    {
                        var page = document.AddPage();
                        page.Width = bitmap.Width;
                        page.Height = bitmap.Height;

                        using (var gfx = XGraphics.FromPdfPage(page))
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                bitmap.Save(ms, ImageFormat.Png);
                                ms.Position = 0;
                                using (var image = XImage.FromStream(ms))
                                {
                                    gfx.DrawImage(image, 0, 0, bitmap.Width, bitmap.Height);
                                }
                            }
                        }
                        document.Save(filename);
                    }
                    return filename;
                }

                string pngPath = Path.Combine(targetFolder, $"{safePrefix}_{timestamp}.png");
                bitmap.Save(pngPath, ImageFormat.Png);
                return pngPath;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ekran görüntüsü dosyaya kaydedilemedi. " + ex.Message, ex);
            }
        }
    }
}
