using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto
{
    public class FaceApiCreateTransDataDto
    {
        public float quality { get; set; }
        public int[] face_rectangle { get; set; }
        public int gender { get; set; }
        public int age { get; set; }
        public int wearmask { get; set; }
        public int wearglass { get; set; }
        public int liveness { get; set; }
        public int lefteye { get; set; }
        public int righteye { get; set; }
        //public string face_landmark { get; set; }
        public int orient { get; set; }
        public float[] angle3D { get; set; }
        //public byte[] feature { get; set; }
    }
}
