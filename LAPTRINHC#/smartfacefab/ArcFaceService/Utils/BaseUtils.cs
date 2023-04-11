using ArcFaceSDK.Utils;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace ArcFaceService.Utils
{
    public class BaseUtils
    {
        /// <summary>
        /// Tải ảnh và resize ra dạng base64
        /// </summary>
        /// <param name="faceInput">Ảnh đầu vào</param>
        /// <param name="faceType">Loại ảnh đầu vào: 1-URL, 2-FILE, 3-BASE64</param>
        /// <returns>Tất cả thuộc tính của khuôn mặt</returns>
        public static string resizeImageFromUrl(string url, int faceType, int orient)
        {
            string imageBase64 = "";

            try
            {
                Image image = getFaceImage(url, faceType);

                int imgWidth = image.Width;
                int imgHeight = image.Height;

                if (imgWidth < 128 || imgHeight < 128)
                {
                    return imageBase64;
                }

                int newWidth = 0;
                int newHeight = 0;

                //Nếu ảnh bị xoay thì phải xoay và resize lại, hiện chỉ fix cho case orient = 2 còn có thể xoay nhiều hướng nữa
                if (orient == 2)
                {
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);

                    newWidth = 128;
                    newHeight = (int)Math.Round((double)(newWidth * imgWidth / imgHeight), MidpointRounding.AwayFromZero);
                }
                else
                {
                    newWidth = 128;
                    newHeight = (int)Math.Round((double)(newWidth * imgHeight / imgWidth), MidpointRounding.AwayFromZero);
                }

                Image imageResize = ResizeImage(image, newWidth, newHeight);
                imageBase64 = ImageToBase64(imageResize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return imageBase64;
        }


        /// <summary>
        /// Resize lại chiều rộng và cao theo đúng tỉ lệ
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static void checkImageWidthAndHeight(ref Image image)
        {
            if (image == null)
            {
                return;
            }
            try
            {
                //if (image.Width > maxWidth || image.Height > maxHeight)
                //{
                //    image = ImageUtil.ScaleImage(image, image.Width, maxHeight);
                //}
                image = ImageUtil.ScaleImage(image, image.Width, image.Height);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public static Image getFaceImage(string faceInput, int faceType)
        {
            Image srcImage = null;

            if (faceType == FaceType.URL)
            {
                using (WebClient webClient = new WebClient())
                {
                    using (MemoryStream ms = new MemoryStream(webClient.DownloadData(faceInput)))
                    {
                        srcImage = Image.FromStream(ms);
                    }
                }

                return srcImage;
            }
            else if (faceType == FaceType.FILE)
            {
                try
                {
                    srcImage = Image.FromFile(faceInput);
                }
                catch
                {
                    throw new Exception(ErrorMessage.ERR_IMAGE_INPUT);
                }
             
            }
            else if (faceType == FaceType.BASE64)
            {
                try
                {
                    byte[] bImg = Convert.FromBase64String(faceInput);
                    using (MemoryStream ms = new MemoryStream(bImg))
                    {
                        return Image.FromStream(ms);
                    }
                }
                catch
                {
                    throw new Exception(ErrorMessage.ERR_IMAGE_INPUT);
                }
            }

            return srcImage;
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static string ImageToBase64(Image image)
        {
            using (MemoryStream m = new MemoryStream())
            {
                image.Save(m, ImageFormat.Jpeg);
                byte[] imageBytes = m.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}
