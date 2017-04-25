namespace TFN.Sts.UI.Register
{
    public class RegisterViewModel : RegisterInputModel
    {
        public string ErrorMessage { get; set; }

        public RegisterViewModel(RegisterInputModel other)
        {
            RegisterEmail = other.RegisterEmail;
            RegisterUsername = other.RegisterUsername;
        }

        public RegisterViewModel() { }
    }
}