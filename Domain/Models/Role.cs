namespace Domain.Models
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
		public virtual ICollection<RolePermission> RolePermissions { get; set; }

	}
}
