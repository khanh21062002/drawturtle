using EPS.Utils.Repository.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class MarkDeleted : IDeleteInfo<User, int>
    {
        public DateTime? DeletedDate { get; set; }
        public int? DeletedUserId { get; set; }
        public virtual User DeletedUser { get; set; }
    }
}
