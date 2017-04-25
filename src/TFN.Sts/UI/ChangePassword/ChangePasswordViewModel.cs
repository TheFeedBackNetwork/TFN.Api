namespace TFN.Sts.UI.ChangePassword
{
    public class ChangePasswordViewModel : ChangePasswordInputModel
    {
        public string ErrorMessage { get; set; }

        public ChangePasswordViewModel(ChangePasswordInputModel other)
        {
            Password = other.Password;
            ConfirmPassword = other.ConfirmPassword;
            ChangePasswordKey = other.ChangePasswordKey;
        }

        public ChangePasswordViewModel()
        {

        }
    }
}