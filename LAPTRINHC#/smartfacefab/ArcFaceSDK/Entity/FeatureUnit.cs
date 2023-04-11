using ArcFaceSDK.SDKModels;

namespace ArcFaceSDK.Entity
{
    public class FeatureUnit
    {
        public float quality { get; set; }
        public int age { get; set; }
        public int gender { get; set; }
        public int mask { get; set; }
        public int liveness { get; set; }
        public int leftEyeClose { get; set; }
        public int rightEyeClose { get; set; }
        public int wearGlass { get; set; }
        public int orient { get; set; }
        public float[] angle3D { get; set; }
        public MRECT faceRect { get; set; }
        public ASF_FaceLandmark[] landmark { get; set; }
        public byte[] feature { get; set; }
    }
}
