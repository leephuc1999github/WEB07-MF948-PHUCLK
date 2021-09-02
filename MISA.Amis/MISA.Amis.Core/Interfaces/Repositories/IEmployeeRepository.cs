using MISA.Amis.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã mới</returns>
        public string GetNewEmployeeCode();

        /// <summary>
        /// Phân trang tìm kiếm nhân viên
        /// </summary>
        /// <param name="keyword">Tìm kiếm theo mã nhân viên, số điện thoại, tên</param>
        /// <param name="pageIndex">Thứ tự trang</param>
        /// <param name="pageSize">Độ dài trang</param>
        /// <returns>Dữ liệu phân tran</returns>
        /// CreatedBy : LP(27/8)
        public BasePaging<Employee> GetEmployeesPaging(string keyword, int pageIndex, int pageSize);

        
    }
}
