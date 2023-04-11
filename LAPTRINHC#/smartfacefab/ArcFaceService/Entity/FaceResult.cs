using System;

namespace ArcFaceService.Entity
{
    public class FaceResult<T1, T2>
    {
        public DateTime timestamp { get; set; }
        public int status { get; set; }
        public string error { get; set; }
        public T1 input_data { get; set; }
        public T2 result { get; set; }
    }
}
