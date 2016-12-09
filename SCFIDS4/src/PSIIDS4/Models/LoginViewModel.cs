using System.ComponentModel.DataAnnotations;

namespace PSIIDS4.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public bool RememberLogin { get; set; }

        public string ReturnUrl { get; set; }
    }

    //public class LoginViewModel : LoginInputModel
    //{
    //    public LoginViewModel()
    //    {
    //    }

    //    public LoginViewModel(LoginInputModel other)
    //    {
    //        Username = other.Username;
    //        Password = other.Password;
    //        RememberLogin = other.RememberLogin;
    //        ReturnUrl = other.ReturnUrl;
    //    }

    //    public string ErrorMessage { get; set; }
    //}
}