using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ProgrammationParallele
{
    public class ImageTool
    {
        public string filePathSource = "./Images_source/";
        public string filePathResult = "./Images_result/";

        /// <summary>
        /// Traitement d'un image
        /// Réduction de résolution + noir et blanc
        /// </summary>
        /// <param name="name"></param>
        /// <param name="saveNamePref"></param>
        public void Imagetraitement(string name, string saveNamePref = null)
        {
            var size = new Size(500, 100);
            var bitMapImage = ResizeImage(LoadImage(name), size);

            SaveBitmapToImage(ChangeInBlackAndWith(bitMapImage), $"{name}_{size}{saveNamePref}");
        }

        /// <summary>
        /// Chargement de l'image
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Image LoadImage(string name)
        {
            return Image.FromFile(string.Concat(filePathSource, $"/{name}"));
        }        

        /// <summary>
        /// Réduction de la résolution de l'image
        /// </summary>
        /// <param name="imgToResize"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private Bitmap ResizeImage(Image imgToResize, Size size)
        {
            //Get the image current width
            int sourceWidth = imgToResize.Width;

            //Get the image current height
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            //Calulate width with new desired size
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            //New Width
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draw image with new width and height
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return b;           
        }

        /// <summary>
        /// Conversion en noir et blanc
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        private Bitmap ChangeInBlackAndWith(Bitmap original)
        {

            Bitmap output = new(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {

                for (int j = 0; j < original.Height; j++)
                {

                    Color c = original.GetPixel(i, j);

                    int average = ((c.R + c.B + c.G) / 3);

                    if (average < 200)
                        output.SetPixel(i, j, Color.Black);

                    else
                        output.SetPixel(i, j, Color.White);

                }
            }

            return output;

        }

        /// <summary>
        /// Sauvegarde de l'image générée
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="fileName"></param>
        private void SaveBitmapToImage(Bitmap bitmap, string fileName)
        {
            bitmap.Save($"{filePathResult + fileName}.jpeg", ImageFormat.Jpeg);
        }
    }
}
