using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FileApi.Helper
{
    public class WriterHelper
    {
        public static string GetFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     
            var gif = Encoding.ASCII.GetBytes("GIF");    
            var png = new byte[] { 137, 80, 78, 71 };              
            var tiff = new byte[] { 73, 73, 42 };                 
            var tiff2 = new byte[] { 77, 77, 42 };                 
            var jpeg = new byte[] { 255, 216, 255, 224 };          
            var jpeg2 = new byte[] { 255, 216, 255, 225 };         

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return "bmp";

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return "gif";

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return "png";

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length))
                || tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return "tiff";

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length))
                || jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return "jpeg";

            return string.Empty;
        }

        public static string GetPathNameFromUrl(string url)
        {
            string filename = Path.GetFileName(new Uri(url).AbsolutePath);
            return filename;
        }


    }
}
