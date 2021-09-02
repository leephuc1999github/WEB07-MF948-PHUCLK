using MISA.Amis.Core.Entites;
using MISA.Amis.Core.Interfaces.Repositories;
using MISA.Amis.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region Declare
        private readonly IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructor
        public DepartmentService(IBaseRepository<Department> baseRepository, IDepartmentRepository departmentRepository) : base(baseRepository)
        {
            this._departmentRepository = departmentRepository;
        }
        #endregion
    }
}
