using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto.Detect
{
    public class DetectFaceTransDataDto
    {
        public Nullable<float> quality { get; set; }
        public int[] face_rectangle { get; set; }
        //public string face_landmark { get; set; }
        public Nullable<int> gender { get; set; }
        public Nullable<int> age { get; set; }
        public Nullable<int> wearmask { get; set; }
        public Nullable<int> wearglass { get; set; }
        public Nullable<int> liveness { get; set; }
        public Nullable<int> lefteye { get; set; }
        public Nullable<int> righteye { get; set; }
        public Nullable<int> orient { get; set; }
        public float[] angle3D { get; set; }
        public byte[] feature { get; set; }
        public string imageBase64 { get; set; }
    }
}
