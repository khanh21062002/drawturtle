using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Utils.Repository.Audit
{
    /// <summary>
    /// Entity need to check for data accessing permission (data permission usually depends on user's unit) should implement this interface
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    public interface IDataProtection<TUserKey> where TUserKey : struct
    {
        /// <summary>
        /// Check user's permission to read the data
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <returns></returns>
        bool CheckPermission(IUserIdentity<TUserKey> userIdentity);
    }
}
