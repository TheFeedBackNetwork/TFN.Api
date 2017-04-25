﻿using System.ComponentModel.DataAnnotations;
using TFN.Mvc.Attributes;

namespace TFN.Sts.UI.SignIn
{
    public class SignInInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength]
        [UsernameOrEmailValid(ErrorMessage = "Invalid username password combination.")]
        public string Username { get; set; }
        [Required]
        [PasswordValid(MinLetters = 1, MinDigits = 1, ErrorMessage = "Invalid username password combination.")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}