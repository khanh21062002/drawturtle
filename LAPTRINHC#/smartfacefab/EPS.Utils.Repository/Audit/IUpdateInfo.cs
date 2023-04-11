using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Utils.Repository.Audit
{
    /// <summary>
    /// Entity need to be extended from this interface to set the user that perform the update automatically
    /// </summary>
    /// <typeparam name="TUser">The user's type that perform the update</typeparam>
    /// <typeparam name="TUserKey">The user's key type</typeparam>
    public interface IUpdateInfo<TUser, TUserKey>
        where TUser : class
        where TUserKey : struct
    {
        DateTime? LastUpdatedDate { get; set; }
        TUserKey? LastUpdatedUserId { get; set; }
        TUser LastUpdatedUser { get; set; }
    }
}
