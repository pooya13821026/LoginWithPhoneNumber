using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Identity
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public Permission? Parent { get; set; }
        public IEnumerable<RolePermission> RolePermissions { get; set;}
    }
}
