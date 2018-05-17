using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace SIGE.Pages.Modulos.Operativo.Reports
{
    /// <summary>
    /// Creado por carlos marin
    /// </summary>
    public class MyImage : System.Web.UI.Page
    {
        private string _pathOutputImage;
        private System.Drawing.Image _inputImage;
        public String _inputImagePath;


        public MyImage(string ImagePath, byte[] byteArrayIn)
        {
            FileStream fc;
            FileStream fs;

            _inputImagePath = ImagePath;
            try
            {
                fc = File.Create(Server.MapPath("temp.jpg"));
                fc.Write(byteArrayIn, 0, byteArrayIn.Length);
                fc.Flush();
                fc.Close();

                fs = new FileStream(Server.MapPath("temp.jpg"), FileMode.Open, FileAccess.Read);

                _inputImage = System.Drawing.Image.FromStream(fs);
                fs.Close();
                _pathOutputImage = _inputImagePath;

            }
            catch
            {

            }
        }

        public void Rotate(string Flip, string imgUrl)
        {
            _pathOutputImage = imgUrl;
            System.Drawing.Image thumb;
            thumb = _inputImage;
            try
            {
                thumb = _inputImage;

                if (Flip == "90")
                {
                    thumb.RotateFlip(RotateFlipType.Rotate90FlipXY);
                }

                if (Flip == "180")
                {
                    thumb.RotateFlip(RotateFlipType.Rotate180FlipXY);
                }

                if (Flip == "270")
                {
                    thumb.RotateFlip(RotateFlipType.Rotate270FlipXY);
                }

                thumb.Save(_pathOutputImage, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch
            {

            }
            finally
            {
                _inputImage = thumb;
                Session["imagenabyte"] = _inputImage;
            }
        }

        public string SaveImage()
        {
            return _pathOutputImage;
        }
        // De image a byte []:
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Session["array"] = ms.ToArray();
            return ms.ToArray();
        }
        // De byte [] a image:
        // este metodo no se usa.
        public System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }


    }
}