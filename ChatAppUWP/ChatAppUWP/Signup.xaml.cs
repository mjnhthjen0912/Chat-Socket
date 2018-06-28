using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ChatAppUWP
{
    public sealed partial class Signup : UserControl
    {
        public Signup()
        {
            this.InitializeComponent();

        }

        private async void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            Login.signUpFadeSt.Value = 0;
            await Task.Delay(200);
            Login.signUpSt.Visibility = Visibility.Collapsed;
            Login.signInSt.Visibility = Visibility.Visible;
            Login.signInFadeSt.Value = 1;
        }

        private void txtAccount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAccount.Text.Length < 6)
            {
                checkAcount.Fill = new SolidColorBrush(Color.FromArgb(180, 240, 65, 0));
                btnSignUp.IsEnabled = false;
            }
            else
            {
                checkAcount.Fill = new SolidColorBrush(Color.FromArgb(180, 0, 240, 54));
                if (txtPassword.Password.Length >= 6)
                    btnSignUp.IsEnabled = true;
            }
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password.Length < 6)
            {
                checkPassword.Fill = new SolidColorBrush(Color.FromArgb(180, 240, 65, 0));
                btnSignUp.IsEnabled = false;
            }
            else
            {
                checkPassword.Fill = new SolidColorBrush(Color.FromArgb(180, 0, 240, 54));
                if (txtAccount.Text.Length >= 6)
                    btnSignUp.IsEnabled = true;
            }
        }

        private async void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            string res = await Login.SignUp(txtAccount.Text, txtPassword.Password);
            var dialog = new MessageDialog(res);
            await dialog.ShowAsync();
        }
    }
}
