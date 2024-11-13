using System.ComponentModel.DataAnnotations;

namespace ProductSimple_Backend.Domain
{
	public class Role
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

		public List<User> Users { get; set; }
	}
}
