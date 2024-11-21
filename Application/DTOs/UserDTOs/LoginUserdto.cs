﻿using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserDTOs
{
    public class LoginUserdto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
