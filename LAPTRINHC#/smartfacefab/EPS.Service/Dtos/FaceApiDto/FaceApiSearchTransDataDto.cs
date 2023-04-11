using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto
{
    public class FaceApiSearchTransDataDto
    {
        public Nullable<float> Quality { get; set; }
        public int[] face_rectangle { get; set; }
        //public string Landmark { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> WearMask { get; set; }
        public Nullable<int> WearGlass { get; set; }
        public Nullable<int> Liveness { get; set; }
        public Nullable<int> LeftEye { get; set; }
        public Nullable<int> RightEye { get; set; }
        public Nullable<int> Orient { get; set; }
        public float[] Angle3D { get; set; }
        public byte[] feature { get; set; }
        public string imageBase64 { get; set; }

    }
}
