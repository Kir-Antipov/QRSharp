using System;
using QR.Core;
using System.IO;
using CommandLine;

#if NETFRAMEWORK
using System.Drawing;
#else
using SixLabors.ImageSharp;
using SixLabors.Primitives;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
#endif

namespace QR
{
    public class Program
    {
        public static void Main(string[] args) => 
            Parser.Default.ParseArguments<Options>(args).WithParsed(options => new Program().Run(options));

        private void Run(Options options)
        {
            string fileName = options.Output;
            if (Path.GetExtension(fileName).ToLower() != ".png")
                fileName += ".png";

            string outputDirectory = Path.Combine(Environment.CurrentDirectory, Path.GetDirectoryName(fileName));
            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            QRCode qr = QRCode.EncodeText(options.Input);
            int scale = Math.Max(options.Scale, 1);
            int border = Math.Max(options.Border, 0);
            int size = scale * qr.Size + 2 * border;

            int foreground = (int)(options.Foreground | 0xFF000000);
            int background = (int)(options.Background | 0xFF000000);

#if NETFRAMEWORK
            Brush fore = new SolidBrush(Color.FromArgb(foreground));
            Brush back = new SolidBrush(Color.FromArgb(background));

            using (Bitmap bit = new Bitmap(size, size))
            {
                using (Graphics g = Graphics.FromImage(bit))
                {
                    g.FillRectangle(back, 0, 0, size, size);
                    qr.Paint((x, y) => g.FillRectangle(fore, border + x * scale, border + y * scale, scale, scale));
                }

                bit.Save(fileName);
            }
#else
            Rgba32 fore = new Rgba32((byte)((foreground & 0xFF0000) >> 16), (byte)((foreground & 0x00FF00) >> 8), (byte)(foreground & 0x0000FF));
            Rgba32 back = new Rgba32((byte)((background & 0xFF0000) >> 16), (byte)((background & 0x00FF00) >> 8), (byte)(background & 0x0000FF));

            using (Image<Rgba32> img = new Image<Rgba32>(size, size))
            {
                img.Mutate(context => {
                    context.Fill(back);
                    qr.Paint((x, y) => context.Fill(fore, new RectangleF(border + x * scale, border + y * scale, scale, scale)));
                });

                img.Save(fileName);
            }
#endif
        }
    }
}
