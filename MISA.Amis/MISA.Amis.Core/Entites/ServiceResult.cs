using MISA.Amis.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Entites
{
    public class ServiceResult
    {
        #region Property
        /// <summary>
        /// Mã kết quả
        /// </summary>
        /// CreatedBy : LP(26/8)
        public int ResultCode { get; set; }

        /// <summary>
        /// Thông báo cho dev
        /// </summary>
        /// CreatedBy : LP(26/8)
        public string UserMessage { get; set; }

        /// <summary>
        /// Message trả về cho client
        /// </summary>
        /// CreatedBy : LP(26/8)
        public List<string> DevMessage { get; set; }

        /// <summary>
        /// Thông tin khác
        /// </summary>
        /// CreatedBy : LP(26/8)
        public string MoreInfo { get; set; }

        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        /// CreatedBy : LP(26/8)
        public object Data { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Khởi tạo đối tượng dịch vụ 
        /// </summary>
        /// CreatedBy : LP(26/8)
        public ServiceResult()
        {
            this.ResultCode = (int)ServiceStatus.Success;
            this.DevMessage = new List<string>();
            this.UserMessage = null;
            this.MoreInfo = null;
            this.Data = null;
        }
        #endregion

        #region Method
        /// <summary>
        /// Gán log thực hiện thành công
        /// </summary>
        /// <param name="serviceResult">Kết quả api trả về</param>
        /// <param name="data">Dữ liệu </param>
        /// CreatedBy : LP(26/8)
        public void SetSuccess(ServiceResult serviceResult, object data)
        {
            serviceResult.ResultCode = (int)ServiceStatus.Success;
            serviceResult.DevMessage.Add(Properties.Resource.Dev_Success_Msg);
            serviceResult.UserMessage = Properties.Resource.User_Success_Msg;
            serviceResult.Data = data;
        }

        /// <summary>
        /// Gán log thực hiện thất bại
        /// </summary>
        /// <param name="serviceResult">Kết quả api trả về</param>
        /// CreatedBy : LP(26/8)
        public void SetNoContent(ServiceResult serviceResult)
        {
            serviceResult.ResultCode = (int)ServiceStatus.NoContent;
            serviceResult.DevMessage.Add(Properties.Resource.Dev_Error_Msg);
            serviceResult.UserMessage = Properties.Resource.User_Info_Msg;
        }

        /// <summary>
        /// Gán log lỗi máy chủ
        /// </summary>
        /// <param name="serviceResult">Kết quả api trả về</param>
        /// CreatedBy : LP(26/8)
        public void SetInternalServerError(ServiceResult serviceResult)
        {
            serviceResult.ResultCode = (int)ServiceStatus.InternalServerError;
            serviceResult.UserMessage = Properties.Resource.User_Error_Msg;
        }

        /// <summary>
        /// Gán log lỗi dữ liệu đầu vào không hợp lệ
        /// </summary>
        /// <param name="serviceResult">Kết quả api trả về</param>
        /// CreatedBy : LP(26/8)
        public void SetBadRequest(ServiceResult serviceResult)
        {
            serviceResult.ResultCode = (int)ServiceStatus.BadRequest;
            serviceResult.UserMessage = Properties.Resource.User_Info_Msg;
        }
        #endregion
    }
}
