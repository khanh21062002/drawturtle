using System;
using System.Drawing;

namespace ArcFaceSDK.Entity
{
    public class ImageRecognizeModel
    {
        public Image image { get; set; }
        public MultiFaceInfo multiFaceInfo { get; set; }
        public FeatureUnit faceFeatureUnit { get; set; }
        public DateTime initTime { get; set; }
        public bool extractAll { get; set; }
        public bool complete { get; set; }

        public ImageRecognizeModel()
        {
        }

        public ImageRecognizeModel( Image image, MultiFaceInfo multiFaceInfo, FeatureUnit faceFeatureUnit, bool extractAll, bool complete)
        {
            this.image = image;
            this.multiFaceInfo = multiFaceInfo;
            this.faceFeatureUnit = faceFeatureUnit;
            this.initTime = DateTime.Now;
            this.extractAll = extractAll;
            this.complete = complete;
        }
    }    
}
