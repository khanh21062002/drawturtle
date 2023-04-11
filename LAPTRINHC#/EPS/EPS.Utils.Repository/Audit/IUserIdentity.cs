using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace EPS.Utils.Repository.Audit
{
    /// <summary>
    /// Base user's information which will be passed to repository
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    public interface IUserIdentity<TUserKey> where TUserKey : struct
    {
        TUserKey UserId { get; }
        string Username { get; }
        List<string> Privileges { get; }
        bool IsAdministrator { get; }
        int CompanyId { get; }

    }
}
