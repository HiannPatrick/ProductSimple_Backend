using System.ComponentModel.DataAnnotations;

namespace ProductSimple_Backend.Domain
{
	public class UserDto
	{
		public int Id { get; set; }

		public string Email { get; set; }

        public UserDto(User user)
        {
            Id = user.Id;
			Email = user.Email;
        }
    }
}
