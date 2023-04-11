using EPS.Data.Entities;
using EPS.Utils.Repository;
using EPS.Utils.Repository.Audit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Data
{
    public class EPSRepository : Repository<EPSContext, User, int>
    {
        public EPSRepository(EPSContext dbContext, IUserIdentity<int> currentUser, ILogger<EPSRepository> logger) : base(dbContext, currentUser, logger)
        {

        }
    }
}
