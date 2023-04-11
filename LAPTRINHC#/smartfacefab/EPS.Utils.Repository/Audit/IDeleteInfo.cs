using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Utils.Repository.Audit
{
    /// <summary>
    /// Entity need to be extended from this interface to set the user that delete the record automatically. The record should be marked as deleted only, not actual deleted from database
    /// </summary>
    /// <typeparam name="TUser">The user's type that perform the deletion</typeparam>
    /// <typeparam name="TUserKey">The user's key type</typeparam>
    public interface IDeleteInfo<TUser, TUserKey>
        where TUser : class
        where TUserKey : struct
    {
        DateTime? DeletedDate { get; set; }
        TUserKey? DeletedUserId { get; set; }
        TUser DeletedUser { get; set; }
    }
}
