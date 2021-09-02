using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.Core.Entites;
using MISA.Amis.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Amis.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController<Department>
    {
        #region Declare
        private readonly IDepartmentService _departmentService;
        #endregion
        
        #region Constructor
        public DepartmentsController(IBaseService<Department> baseService, IDepartmentService departmentService) : base(baseService)
        {
            this._departmentService = departmentService;
        }
        #endregion
    }
}
