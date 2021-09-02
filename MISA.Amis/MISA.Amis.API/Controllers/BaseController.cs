using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Amis.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Declare
        private readonly IBaseService<T> _baseService;
        #endregion

        #region Contructor
        /// <summary>
        /// Khởi tạo đối tượng
        /// </summary>
        /// <param name="baseService">Dịch vụ</param>
        /// CreatedBy : LP(26/8)
        public BaseController(IBaseService<T> baseService)
        {
            this._baseService = baseService;
        }
        #endregion

        #region Method
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Trả về dịch vụ lấy tất cả bản ghi</returns>
        /// CreatedBy : LP(26/8)
        [HttpGet]
        public IActionResult GetAll()
        {
            var serviceResult = _baseService.GetAll();
            return Ok(serviceResult);
        }

        /// <summary>
        /// Lấy một bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Trả về dịch vụ lấy một bản ghi</returns>
        /// CreatedBy : LP(26/8)
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var serviceResult = _baseService.GetById(id);
            return Ok(serviceResult);
        }

        /// <summary>
        /// Lấy danh sách những bản ghi có giá trị 
        /// </summary>
        /// <param name="name">Tên cột</param>
        /// <param name="value">Giá trị cột</param>
        /// <returns>Trả về dịch vụ lấy những bản ghi có thuộc tính giống nhau</returns>
        /// CreatedBy : LP(26/8)
        [HttpGet]
        [Route("Property")]
        public IActionResult GetByProperty(string name, string value)
        {
            var serviceResult = _baseService.GetByNameAndValueProperty(name, value);
            return Ok(serviceResult);
        }

        /// <summary>
        /// Thêm một bản ghi 
        /// </summary>
        /// <param name="entity">Thông tin đối tượng</param>
        /// <returns>Trả về dịch vụ thêm mới bản ghi</returns>
        /// CreatedBy : LP(26/8)
        [HttpPost]
        public IActionResult Insert(T entity)
        {
            var resultService = _baseService.Insert(entity);
            return Ok(resultService);
        }

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <param name="entity">Thông tin đối tượng</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, T entity)
        {
            var serviceResult = _baseService.Update(id, entity);
            return Ok(serviceResult);
        }

        /// <summary>
        /// Xóa một bản ghi 
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Dịch vụ xóa một bản ghi</returns>
        /// CreatedBy : LP(26/8)
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var serviceResult = _baseService.Delete(id);
            return Ok(serviceResult);
        }

        #endregion
    }
}
