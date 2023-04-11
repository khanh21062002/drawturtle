namespace ArcFaceService.Utils
{
    public class CompareInput
    {
        public ImageInput sourceImg;
        public ImageInput destImg;

        public CompareInput(ImageInput sourceImg, ImageInput destImg)
        {
            this.sourceImg = sourceImg;
            this.destImg = destImg;
        }
    }
}
