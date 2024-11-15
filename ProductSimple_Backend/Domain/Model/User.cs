﻿using System.ComponentModel.DataAnnotations;

namespace ProductSimple_Backend.Domain
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string PasswordHash { get; set; }

		public List<Role> Roles { get; set; }
	}
}
