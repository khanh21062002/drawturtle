using System;

namespace ArcFaceSDK.Entity
{
    public class EngineModel
    {
        public IntPtr PtrEngine { get; set; }
        public bool EngineFree { get; set; }

        public EngineModel(IntPtr PtrEngine, bool EngineFree)
        {
            this.PtrEngine = PtrEngine;
            this.EngineFree = EngineFree;
        }
    }
}
