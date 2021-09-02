using MISA.Amis.Core.Entites;
using MISA.Amis.Core.Interfaces.Repositories;
using MISA.Amis.Core.Interfaces.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Declare
        private readonly IEmployeeRepository _employeeRepository;
        private const string _nameExcelFile = "Danh_sach_nhan_vien.xlsx";
        private const string _nameSheet = "DANH SÁCH NHÂN VIÊN";
        #endregion

        #region Constructor
        /// <summary>
        /// Khởi tạo dịch vụ cho nhân viên
        /// </summary>
        /// <param name="baseRepository"></param>
        /// <param name="employeeRepository"></param>
        /// CreatedBy : LP(26/8)
        public EmployeeService(IBaseRepository<Employee> baseRepository, IEmployeeRepository employeeRepository) : base(baseRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        #endregion

        #region Method
        /// <summary>
        /// Dịch vụ sinh mã nhân viên mới
        /// </summary>
        /// <returns>Kết quả sinh mã mới</returns>
        /// CreatedBy : LP(26/8)
        public ServiceResult GetNewEmployeeCode()
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            try
            {
                var employee = _employeeRepository.GetNewEmployeeCode();
                if (employee != null)
                {
                    serviceResult.SetSuccess(serviceResult, employee);
                }
                else
                {
                    serviceResult.SetNoContent(serviceResult);
                }
            }
            catch (Exception ex)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add(ex.Message);
            }
            return serviceResult;
        }


        /// <summary>
        /// Phân trang tìm kiếm nhân viên
        /// </summary>
        /// <param name="keyword">Tìm kiếm theo tên , mã, sđt</param>
        /// <param name="pageIndex">Thứ tự trang</param>
        /// <param name="pageSize">Số bản ghi trên một trang</param>
        /// <returns></returns>
        public ServiceResult GetEmployeesPaging(string keyword, int pageIndex, int pageSize)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            try
            {
                var employees = _employeeRepository.GetEmployeesPaging(keyword, pageIndex, pageSize);
                if (employees.Data.Count() > 0)
                {
                    serviceResult.SetSuccess(serviceResult, employees);
                }
                else
                {
                    serviceResult.SetNoContent(serviceResult);
                }
            }
            catch (Exception ex)
            {
                serviceResult.DevMessage.Add($"Exception {ex.Message}");
                serviceResult.SetInternalServerError(serviceResult);
            }
            return serviceResult;
        }

        /// <summary>
        /// Xuất file excel danh sách nhân viên
        /// </summary>
        /// <param name="file">Thông tin file nhân viên</param>
        /// <returns>Dịch vụ xuất danh sách excel</returns>
        /// CreatedBy : LP(26/8)
        public ServiceResult ExportExcelData(FileInfo file)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            try
            {
                using (var package = new ExcelPackage(file))
                {
                    var workSheet = package.Workbook.Worksheets.Add(_nameSheet);

                    workSheet.Cells["A1:I1"].Merge = true;
                    workSheet.Cells["A1:I1"].Value = "DANH SÁCH NHÂN VIÊN";
                    workSheet.Cells["A1:I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells["A1:I1"].Style.Font.Name = "Arial";
                    workSheet.Cells["A1:I1"].Style.Font.Size = 16;
                    workSheet.Cells["A1:I1"].Style.Font.Bold = true;
                    workSheet.Cells["A2:I2"].Merge = true;

                    // tiêu đề bảng excel
                    workSheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Row(3).Style.Font.Bold = true;
                    workSheet.Row(3).Style.Font.Name = "Arial";
                    workSheet.Row(3).Style.Font.Size = 10;

                    // đổ màu cho tiêu đề
                    workSheet.Cells["A3:I3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells["A3:I3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#d8d8d8"));



                    workSheet.Cells[3, 1].Value = "STT";
                    workSheet.Cells[3, 2].Value = "Mã nhân viên";
                    workSheet.Cells[3, 3].Value = "Tên nhân viên";
                    workSheet.Cells[3, 4].Value = "Giới tính";
                    workSheet.Cells[3, 5].Value = "Ngày sinh";
                    workSheet.Cells[3, 6].Value = "Chức danh";
                    workSheet.Cells[3, 7].Value = "Tên đơn vị";
                    workSheet.Cells[3, 8].Value = "Số tài khoản";
                    workSheet.Cells[3, 9].Value = "Tên ngân hàng";


                    var employees = _baseRepository.GetAll();
                    // bắt đầu từ hàng thứ 4
                    int row = 4;
                    // số thứ tự
                    int count = 1;
                    // duyệt thông tin vào ô trong file excel
                    foreach (var employee in employees)
                    {
                        string dob = Convert.ToDateTime(employee.DateOfBirth).ToString("dd/MM/yyyy");
                        workSheet.Cells[row, 1].Value = count;
                        workSheet.Cells[row, 2].Value = employee.EmployeeCode;
                        workSheet.Cells[row, 3].Value = employee.FullName;
                        workSheet.Cells[row, 4].Value = employee.GenderName;
                        workSheet.Cells[row, 5].Value = employee.DateOfBirth == null ? "" : dob;
                        workSheet.Cells[row, 6].Value = employee.PositionName;
                        workSheet.Cells[row, 7].Value = employee.DepartmentName;
                        workSheet.Cells[row, 8].Value = employee.AccountNumber;
                        workSheet.Cells[row, 9].Value = employee.BankName;

                        workSheet.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        workSheet.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        workSheet.Row(row).Style.Font.Name = "Times New Roman";
                        workSheet.Row(row).Style.Font.Size = 11;
                        row++;
                        count++;
                    }


                    string modelRange = "A3:I" + (employees.Count()+3).ToString();
                    var modelTable = workSheet.Cells[modelRange];

                    // Thêm viền cho các ô
                    modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;


                    workSheet.Column(1).AutoFit();
                    workSheet.Column(2).AutoFit();
                    workSheet.Column(3).AutoFit();
                    workSheet.Column(4).AutoFit();
                    workSheet.Column(5).AutoFit();
                    workSheet.Column(6).AutoFit();
                    workSheet.Column(7).AutoFit();
                    workSheet.Column(8).AutoFit();
                    workSheet.Column(9).AutoFit();
                    package.Save();
                }
                
                serviceResult.SetSuccess(serviceResult, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add(ex.Message);
            }
            return serviceResult;
        }

        /// <summary>
        /// Kiểm tra giá trị trùng
        /// </summary>
        /// <param name="nameProperty"></param>
        /// <param name="valueProperty"></param>
        /// <returns></returns>
        public ServiceResult CheckDuplicate(string nameProperty, string valueProperty, string id)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                Guid employeeId = new Guid();
                if(id != null)
                {
                    employeeId = Guid.Parse(id);
                }
                var result = _baseRepository.IsDuplication(nameProperty, valueProperty, employeeId);
                serviceResult.SetSuccess(serviceResult, result);
            }
            catch (Exception ex)
            {
                serviceResult.DevMessage.Add(ex.Message);
                serviceResult.SetInternalServerError(serviceResult);
            }
            return serviceResult;
        }
        #endregion
    }
}
