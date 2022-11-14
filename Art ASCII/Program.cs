using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Art_ASCII
{
    class Program
    {
        private const double WIDTH_OFFSET = 1.5;
        private const int MAX_WIDTH = 300;

        [STAThread]
        static void Main(string[] args)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Images | *.bmp; *.png; *.jpg; *.JPEG"
            };



            Console.WriteLine("Нажмите Enter: ");

            while (true)
            {
                Console.ReadLine();

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    continue;

                Console.Clear();

                var bitmap = new Bitmap(openFileDialog.FileName);
                bitmap = ResizeBitmap(bitmap);
                bitmap.ToGrayscale();

                var convert = new BitmapToASCIIConvertor(bitmap);
                var rows = convert.Convert();
                var rowNegative = convert.ConvertNegative();

                foreach (var row in rows)
                {
                    Console.WriteLine(row); 
                }


                File.WriteAllLines("image.txt", rowNegative.Select(r => new string(r)));
                Console.SetCursorPosition(0, 0);

            }

           

        }


        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {

            var newHeight = bitmap.Height / WIDTH_OFFSET * MAX_WIDTH / bitmap.Width;
            if (bitmap.Width > MAX_WIDTH || bitmap.Height > newHeight)
                bitmap = new Bitmap(bitmap, new Size(MAX_WIDTH, (int)newHeight));
            return bitmap;


        }
    }
}

