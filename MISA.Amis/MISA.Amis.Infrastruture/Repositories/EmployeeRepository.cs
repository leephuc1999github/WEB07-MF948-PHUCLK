using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Amis.Core.Entites;
using MISA.Amis.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Infrastruture.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Constructor
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
            
        }
        #endregion

        #region Method
        public BasePaging<Employee> GetEmployeesPaging(string keyword, int pageIndex, int pageSize)
        {
            BasePaging<Employee> result = new BasePaging<Employee>();

            string sqlCommand = "Proc_GetEmployeesPaging";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Keyword", keyword);
            parameters.Add("@PageIndex", pageIndex);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add(@"TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            // danh sách nhân viên sau khi phân trang
            var employees = _dbConnection.Query<Employee>(sqlCommand, param: parameters, commandType: CommandType.StoredProcedure);

            // dữ liệu phân trang
            // tổng số bản ghi tìm kiếm
            result.TotalRecord = parameters.Get<int>("TotalRecord");
            // số trang sau khi tìm kiếm
            result.TotalPage = parameters.Get<int>("TotalPage");
            // ds nhân viên
            result.Data = employees;
            // Tổng số bản ghi trên trang hiện tại
            result.TotalRecordOnCurrentPage = employees.Count();
            // Trang số 
            result.PageIndex = pageIndex;
            // Độ lớn bản ghi trên một trang
            result.PageSize = pageSize;

            return result;
        }


        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã mới</returns>
        /// CreatedBy : LP(26/8)
        public string GetNewEmployeeCode()
        {
            // lấy mã lớn nhất
            string oldCode = _dbConnection.QueryFirstOrDefault<string>("Proc_GetNewEmployeeCode", commandType: CommandType.StoredProcedure);
            // sinh mã mới
            string newCode = GenerateEmployeeCode(oldCode);
            return newCode;
        }

        /// <summary>
        /// Tạo mã mới
        /// </summary>
        /// <param name="oldCode">Mã nhân viên cũ</param>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy : LP(27/8)
        private string GenerateEmployeeCode(string oldCode)
        {
            string partNumbers = string.Empty;
            for (int i = 0; i < oldCode.Length; i++)
            {
                // lấy phần số của mã cũ
                if(oldCode[i] >= '0' && oldCode[i] <= '9')
                {
                    partNumbers += oldCode[i];
                }
            }
            // chuyển phần số => số
            int number = partNumbers.Length > 0 ? int.Parse(partNumbers) : 0;
            return "NV-" + (number+1);
            #region OLD
            //string changeValue = "";
            //int plus = 1;
            //int brk = 0;
            //for (int i = oldCode.Length - 1; i >= 0; i--)
            //{
            //    brk = i;
            //    if (oldCode[i] >= '0' && oldCode[i] <= '9')
            //    {
            //        int n = (int)(oldCode[i] - '0');
            //        if (n + plus >= 10)
            //        {
            //            int v = (n + plus) - 10;
            //            plus = 1;
            //            changeValue = v.ToString() + changeValue;
            //        }
            //        else
            //        {
            //            changeValue = (n + plus).ToString() + changeValue;
            //            break;
            //        }
            //    }
            //    else
            //    {
            //        brk += 1;
            //        changeValue = plus.ToString() + changeValue;
            //        break;
            //    }
            //}
            //return oldCode.Substring(0, brk) + changeValue;
            #endregion
        }
        #endregion
    }
}
