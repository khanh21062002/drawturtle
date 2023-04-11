using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Utils.Repository.Audit
{
    public interface IIdentifier<TKey>
    {
        TKey Id { get; set; }
    }

    public interface IIdentifier : IIdentifier<int>
    {

    }
}
