using Microsoft.Extensions.Configuration;
using MISA.Amis.Core.Entites;
using MISA.Amis.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Infrastruture.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        #region Constructor
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion
    }
}
