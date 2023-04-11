namespace ArcFaceService.Utils
{
    public class ImageInput
    {
        public string uri { get; set; }
        public int fileType { get; set; }
        public int[] groupIds { get; set; }
        public float threshold { get; set; }

        public ImageInput()
        {

        }

        public ImageInput(string uri, int fileType)
        {
            this.uri = uri;
            this.fileType = fileType;
        }

        public ImageInput(string uri, int fileType, int[] groupIds, float threshold)
        {
            this.uri = uri;
            this.fileType = fileType;
            this.groupIds = groupIds;
            this.threshold = threshold;
        }
    }
}
