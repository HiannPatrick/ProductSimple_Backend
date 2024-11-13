using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductSimple_Backend.Domain
{
	public class UserRole
	{
		[Required]
		[ForeignKey("User")]
        public int UserId { get; set; }
		public virtual User User { get; set; }

		[Required]
		[ForeignKey("Role")]
        public int RoleId { get; set; }
		public virtual Role Role { get; set; }
	}
}
