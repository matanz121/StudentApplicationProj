using Microsoft.AspNetCore.Components;
using MudBlazor;
using StudentsApplicationProj.Shared.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Client.Pages.UserProfile
{
    public partial class Security
    {
        private readonly ChangePasswordRequest passwordModel = new ChangePasswordRequest();

        private async Task ChangePasswordAsync()
        {
            try
            {
                await _clientAuthService.ChangePasswordAsync(passwordModel);
                _snackbar.Add("Password Changed!", Severity.Success);
                passwordModel.Password = string.Empty;
                passwordModel.NewPassword = string.Empty;
                passwordModel.ConfirmNewPassword = string.Empty;
            }
            catch
            {
                _snackbar.Add("Something went wrong, please try again", Severity.Error);
            }
        }

        private IEnumerable<string> PasswordStrength(string pw)
        {
            if (string.IsNullOrWhiteSpace(pw))
            {
                yield return "Password is required!";
                yield break;
            }
            if (pw.Length < 8)
                yield return "Password must be at least of length 8";
            if (!Regex.IsMatch(pw, @"[A-Z]"))
                yield return "Password must contain at least one capital letter";
            if (!Regex.IsMatch(pw, @"[a-z]"))
                yield return "Password must contain at least one lowercase letter";
            if (!Regex.IsMatch(pw, @"[0-9]"))
                yield return "Password must contain at least one digit";
        }

        private MudTextField<string> pwField;

        private string PasswordMatch(string arg)
        {
            if (pwField.Value != arg)
                return "Passwords don't match";
            return null;
        }
    }
}