using MISA.Amis.Core.Entites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Interfaces.Services
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Tạo mã nhân viên mưới
        /// </summary>
        /// <returns>Kết quả dịch vụ sinh mã mới</returns>
        public ServiceResult GetNewEmployeeCode();

        /// <summary>
        /// Phân trang tìmkieesm nhân viên
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="pageIndex">Thứ tự trang</param>
        /// <param name="pageSize">SỐ bản ghi trên một trang</param>
        /// <returns></returns>
        public ServiceResult GetEmployeesPaging(string keyword, int pageIndex, int pageSize);

        /// <summary>
        /// Xuất file excel theo phân trang
        /// </summary>
        /// CreatedBy : LP(27/8)
        public ServiceResult ExportExcelData(FileInfo file);

        /// <summary>
        /// Kiểm tra thuộc tính đã tồn tại
        /// </summary>
        /// <param name="nameProperty"></param>
        /// <param name="valueProperty"></param>
        /// <returns></returns>
        public ServiceResult CheckDuplicate(string nameProperty, string valueProperty, string id);
    }
}
