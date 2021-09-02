using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Entites
{
    public class Department : BaseEntity
    {
        /// <summary>
        /// Đơn vị id
        /// </summary>
        /// CreatedBy : LP(26/8)
        [Key]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// CreatedBy : LP(26/8)
        [Required]
        [Duplication]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        /// CreatedBy : LP(26/8)
        [Required]
        public string DepartmentName { get; set; }
    }
}
