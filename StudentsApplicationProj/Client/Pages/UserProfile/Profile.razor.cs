using StudentsApplicationProj.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.IO;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Client.Pages.UserProfile
{
    public partial class Profile
    {
        private readonly UpdateProfileRequest profileModel = new UpdateProfileRequest();

        private async Task UpdateProfileAsync(string firstName, string lastName)
        {
            UpdateProfileRequest updateProfileModel = new UpdateProfileRequest
            {
                FirstName = firstName,
                LastName = lastName
            };
            try
            {
                await _clientAuthService.UpdateProfileAsync(updateProfileModel);
                _snackbar.Add("Your Profile has been updated, please re-login.", Severity.Success);
                navigationManager.NavigateTo("/auth/logout");
            }
            catch
            {
                _snackbar.Add("Something went wrong, please try again", Severity.Error);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await LoadDataAsync();
            }
            catch
            {
                _snackbar.Add("Error loading user information, please try again", Severity.Error);
            }
        }

        private async Task LoadDataAsync()
        {
            var user = _clientAuthService.User;
            //profileModel.Email = user.Email;
            profileModel.FirstName = user.Name;
            //profileModel.LastName = user.LastName;
        }
    }
}