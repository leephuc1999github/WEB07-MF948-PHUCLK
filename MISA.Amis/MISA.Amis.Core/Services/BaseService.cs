using MISA.Amis.Core.Entites;
using MISA.Amis.Core.Enumerations;
using MISA.Amis.Core.Interfaces.Repositories;
using MISA.Amis.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Services
{
    public class BaseService<T> : IBaseService<T>
    {
        #region Property
        protected IBaseRepository<T> _baseRepository;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<T> baseRepository)
        {
            this._baseRepository = baseRepository;
        }
        #endregion

        #region Method
        /// <summary>
        /// Xóa một đối tượng
        /// </summary>
        /// <param name="id">id đối tượng cần xóa</param>
        /// <returns></returns>
        /// CreatedBy : LP(26/8)
        public ServiceResult Delete(Guid id)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.DELETE;
            try
            {
                var entity = _baseRepository.GetById(id);
                if (entity != null)
                {
                    int rowEffects = _baseRepository.Delete(id);
                    if (rowEffects > 0)
                    {
                        serviceResult.SetSuccess(serviceResult, rowEffects);
                    }
                    else
                    {
                        serviceResult.SetBadRequest(serviceResult);
                        serviceResult.DevMessage.Add(string.Format(Properties.Resource.Delete_Error_Msg, id.ToString()));
                    }
                }
                else
                {
                    serviceResult.SetBadRequest(serviceResult);
                    serviceResult.UserMessage = "Dữ liệu không tồn tại trong hệ thống!";
                    serviceResult.DevMessage.Add(string.Format(Properties.Resource.NotFound_Msg, id.ToString()));
                }
            }
            catch (Exception ex)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add($"Exception {ex.Message}");
            }
            return serviceResult;
        }

        /// <summary>
        /// Lấy tất cả bản ghi trong csdl
        /// </summary>
        /// <returns></returns>
        /// CreatedBy : LP(26/8)
        public ServiceResult GetAll()
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            try
            {
                var result = _baseRepository.GetAll();
                if (result.Count() > 0)
                {
                    serviceResult.SetSuccess(serviceResult, result);
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
        /// Lấy một bản ghi theo id
        /// </summary>
        /// <param name="id">id đối tượng cần tìm</param>
        /// <returns></returns>
        /// CreatedBy : LP(26/8)
        public ServiceResult GetById(Guid id)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            try
            {
                var result = _baseRepository.GetById(id);

                if (result != null)
                {
                    serviceResult.SetSuccess(serviceResult, result);
                }
                else
                {
                    serviceResult.SetBadRequest(serviceResult);
                    serviceResult.DevMessage.Add(string.Format(Properties.Resource.NotFound_Msg, id.ToString()));
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
        /// CHèn một đối tượng 
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns></returns>
        /// CreatedBy : LP(26/8)
        public ServiceResult Insert(T entity)
        {
            ServiceResult serviceResult = this.CheckValidate(new Guid(), entity);
            serviceResult.MoreInfo = Properties.Resource.POST;
            if (serviceResult.ResultCode == (int)ServiceStatus.Success)
            {
                try
                {
                    int rowEffects = _baseRepository.Insert(entity);
                    if (rowEffects > 0)
                    {
                        serviceResult.SetSuccess(serviceResult, rowEffects);
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

            }
            return serviceResult;
        }

        /// <summary>
        /// Cập nhật một đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <param name="entity">Thông tin đối tượng</param>
        /// <returns></returns>
        /// CreatedBy : LP(26/8)
        public ServiceResult Update(Guid id, T entity)
        {
            ServiceResult serviceResult = this.CheckValidate(id, entity);
            serviceResult.MoreInfo = Properties.Resource.PUT;
            if (serviceResult.ResultCode == (int)ServiceStatus.Success)
            {
                try
                {
                    var oldEntity = _baseRepository.GetById(id);
                    if (oldEntity != null)
                    {
                        // kiểm tra trường mã có trong csdl ?
                        int rowEffects = _baseRepository.Update(id, entity);
                        if (rowEffects > 0)
                        {
                            serviceResult.SetSuccess(serviceResult, rowEffects);
                        }
                        else
                        {
                            serviceResult.SetNoContent(serviceResult);
                        }
                    }
                    else
                    {
                        serviceResult.SetNoContent(serviceResult);
                        serviceResult.DevMessage.Add(string.Format(Properties.Resource.NotFound_Msg, id.ToString()));
                    }

                }
                catch (Exception ex)
                {
                    serviceResult.SetInternalServerError(serviceResult);
                    serviceResult.DevMessage.Add(ex.Message);
                }
            }
            return serviceResult;
        }

        /// <summary>
        /// Kiểm tra đầu vào dữ liệu 
        /// </summary>
        /// <param name="entity">Thông tin đối tượng </param>
        /// <returns>Thông tin sau khi validate</returns>
        /// CreatedBy : LP(26/8)
        public ServiceResult CheckValidate(Guid id, T entity)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                foreach (PropertyInfo property in entity.GetType().GetProperties())
                {
                    // gán giá trị khóa
                    if (IsKey(property))
                    {
                        property.SetValue(entity, id);
                    }
                    // kiểm tra thuộc tính required
                    if (property.IsDefined(typeof(Required), true))
                    {
                        if (IsNull(property, entity))
                        {
                            serviceResult.ResultCode = (int)ServiceStatus.BadRequest;
                            serviceResult.DevMessage.Add(string.Format(Properties.Resource.Required_Msg, property.Name));
                            serviceResult.UserMessage = string.Format(Properties.Resource.User_Info_Msg, property.Name);
                        }

                    }
                    // kiểm tra giá trị  trùng
                    if (property.IsDefined(typeof(Duplication), true))
                    {
                        string name = property.Name;
                        var value = property.GetValue(entity);
                        if (_baseRepository.IsDuplication(property.Name, (string)value, id))
                        {
                            serviceResult.ResultCode = (int)ServiceStatus.BadRequest;
                            serviceResult.DevMessage.Add(string.Format(Properties.Resource.Duplicate_Msg, name));
                            serviceResult.UserMessage = string.Format(Properties.Resource.User_Info_Msg, name);
                        }

                    }
                    // kiểm tra email
                    if (property.IsDefined(typeof(Email), true))
                    {
                        if (property.GetValue(entity) != null)
                        {
                            if (IsNotEmail(property, entity))
                            {
                                serviceResult.ResultCode = (int)ServiceStatus.BadRequest;
                                serviceResult.DevMessage.Add(string.Format(Properties.Resource.Email_Msg, property.Name));
                                serviceResult.UserMessage = string.Format(Properties.Resource.Email_Msg, property.Name);
                            }
                        }


                    }
                    // kiểm tra mapping
                    if (property.IsDefined(typeof(Mapping), true))
                    {
                        string tableMapping = "Department";
                        if (!IsMapping(property, entity, tableMapping))
                        {
                            serviceResult.ResultCode = (int)ServiceStatus.BadRequest;
                            serviceResult.DevMessage.Add(string.Format(Properties.Resource.Mapping_Error_Msg, property.Name, tableMapping));
                            serviceResult.UserMessage = Properties.Resource.User_Info_Msg;
                        }
                    }
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
        /// Thuộc tính là khóa
        /// </summary>
        /// <param name="property">Thuộc tính</param>
        /// <returns>true nếu là khóa ngược lại false nếu không phải khóa</returns>
        /// CreatedBy : LP(26/8)
        private bool IsKey(PropertyInfo property)
        {
            return property.IsDefined(typeof(Key), true);
        }

        /// <summary>
        /// Kiểm tra giá trị null
        /// </summary>
        /// <returns>true nếu giá trị rỗng ngược lại false nếu có giá trị</returns>
        /// CreatedBy : LP(26/8)
        private bool IsNull(PropertyInfo property, T entity)
        {
            var value = property.GetValue(entity);
            if (string.IsNullOrEmpty(Convert.ToString(value)) || value == null) return true;
            return false;
        }

        /// <summary>
        /// Kiểm tra định dạng email
        /// </summary>
        /// <returns>true nếu giá trị không phải email ngược lại false nếu là email</returns>
        /// CreatedBy : LP(26/8)
        private bool IsNotEmail(PropertyInfo property, T entity)
        {
            var email = property.GetValue(entity);

            string pattern = @"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;,.]{0,1}\s*)+$";
            Regex regex = new Regex(pattern);

            if (regex.Matches((string)email).Count == 0)
                return true;

            return false;
        }

        /// <summary>
        /// Kiểm tra giá trị mapping
        /// </summary>
        /// <param name="property">Thuộc tính mapping</param>
        /// <param name="entity">Đối tượng</param>
        /// <param name="tableMapping">Bảng được mapping</param>
        /// <returns>true nếu mapping thành công false nếu mapping không thành công</returns>
        /// CreatedBy : LP(26/8)
        private bool IsMapping(PropertyInfo property, T entity, string tableMapping)
        {
            var value = property.GetValue(entity);
            var result = _baseRepository.IsMapping(tableMapping, value.ToString());
            return result;
        }

        /// <summary>
        /// Lấy đối tượng theo cặp dữ liệu : tên cột và giá trị
        /// </summary>
        /// <param name="name">Tên cột</param>
        /// <param name="value">Giá trị cột</param>
        /// <returns></returns>
        /// CreatedBy : LP(26/8)
        public ServiceResult GetByNameAndValueProperty(string name, string value)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            try
            {
                var entity = _baseRepository.GetByProperty(name, value);
                if (entity != null)
                {
                    serviceResult.SetSuccess(serviceResult, entity);
                }
                else
                {
                    serviceResult.SetNoContent(serviceResult);
                }
            }
            catch (Exception ex)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add($"Exception {ex.Message}");
            }
            return serviceResult;
        }
        #endregion
    }
}
