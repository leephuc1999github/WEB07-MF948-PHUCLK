using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Interfaces.Repositories
{

    /// <summary>
    /// Repository generic interface
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    /// CreatedBy : LP(26/8)
    public interface IBaseRepository<T>
    {
        #region Methods
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Các bản ghi bị ảnh hưởng</returns>
        /// CreatedBy : LP(26/8)
        public IEnumerable<T> GetAll();

        /// <summary>
        /// Phân trang và tìm kiếm đối tượng
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="pageIndex">Thứ tự trang</param>
        /// <param name="pageSize">Độ dài bản ghi trên một trang</param>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy : LP(26/8)
        public IEnumerable<T> GetPaging(string keyword, int pageIndex, int pageSize);

        /// <summary>
        /// Lấy một bản ghi
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Trả về các bản ghi ảnh hưởng</returns>
        /// CreatedBy : LP(26/8)
        public T GetById(Guid id);

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entity">Thông tin đối tượng</param>
        /// <returns>Trạng thái sau khi thêm</returns>
        /// CreatedBy : LP(26/8)
        public int Insert(T entity);

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns>Trạng thái bản ghi sau khi cập nhật</returns>
        /// CreatedBy : LP(26/8)
        public int Update(Guid id, T entity);

        /// <summary>
        /// XÓa một bản ghi
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <returns>Trạng thái sau khi xóa</returns>
        /// CreatedBy : LP(26/8)
        public int Delete(Guid id);

        /// <summary>
        /// Lấy danh sách bản ghi theo tên thuộc tính 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        /// CreatedBy : LP(26/8)
        public IEnumerable<T> GetByProperty(string propertyName, string propertyValue);

        /// <summary>
        /// Kiểm tra trùng giá trị
        /// </summary>
        /// <param name="name">Tên cột</param>
        /// <param name="value">Giá trị cột</param>
        /// <returns></returns>
        public bool IsDuplication(string name, string value, Guid id);

        /// <summary>
        /// Kiểm tra giá trị mapping với bảng khác
        /// </summary>
        /// <param name="tableMapping">Tên bảng mapping</param>
        /// <param name="value">Giá trị mapping</param>
        /// <returns>true nếu giá trị tồn tại trong bảng mapping ngược lại faile nếu không tồn tại</returns>
        /// CreatedBy :LP(27/8)
        public bool IsMapping(string tableMapping, string value);
        #endregion
    }
}

