using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class CamViewConfig
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? DeviceId { get; set; }
        public int? Position { get; set; }
        public string StrConfig { get; set; }
        public string Description { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("DeviceId")]
        public virtual Device Device { get; set; }
    }
}
