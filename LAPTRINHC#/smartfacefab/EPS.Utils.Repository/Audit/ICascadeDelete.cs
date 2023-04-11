using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Utils.Repository.Audit
{
    public interface ICascadeDelete
    {
        /// <summary>
        /// Excuted before deleting an entity. Use this to check the entity is "safe" to delete, or removing the relationship with other entites before deleting.
        /// </summary>
        void OnDelete();
    }
}
