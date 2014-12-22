namespace ProductionMan.Desktop.Controls.Authentication
{

    public class LoginWindowSelector : BaseContentSelector<Domain.Security.User.LoginStates>
    {
    
        public LoginWindowSelector(LoginWindowFactory factory)
        {
            var loginContent = factory.CreateLoginPage();
            var errorContent = factory.CreateLoginStatusMessagePage();
            var signingInContent = factory.CreateLoginProgressPage();
            var signedInContent = factory.CreateLoginLoadinPage();

            AddContent(Domain.Security.User.LoginStates.NeverSignedIn, loginContent);
            AddContent(Domain.Security.User.LoginStates.IncorrectCredentials, errorContent);
            AddContent(Domain.Security.User.LoginStates.Error, errorContent);
            AddContent(Domain.Security.User.LoginStates.SigningIn, signingInContent);
            AddContent(Domain.Security.User.LoginStates.SignedIn, signedInContent);
        }
    }
}
