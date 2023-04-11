using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.CamViewConfig
{
    public class CamViewConfigUpdateDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? DeviceId { get; set; }
        public int? Position { get; set; }
        public string StrConfig { get; set; }
        public string Description { get; set; }
    }
}
