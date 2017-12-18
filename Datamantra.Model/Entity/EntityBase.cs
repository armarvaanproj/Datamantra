using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datamantra.Model.Entity
{
    public class EntityBase
    {
        [StringLength(8)]
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        [StringLength(8)]
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public StatusType Status { get; set; }
        public bool IsDeleted { get; set; }
        public long UserId { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
    }

    public enum StatusType
    {
        Failure,
        Success,
        Warning,
        Info,
        Duplicate,
        ContainsAnotherTable,
        NotExist
    }
    public enum RoleType
    {
        ADMIN = 1,
        STUDENT = 3,
        STAFF = 2,

    }
}
