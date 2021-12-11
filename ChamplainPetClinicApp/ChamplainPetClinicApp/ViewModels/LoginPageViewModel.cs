using ChamplainPetClinicApp.Models;
using ChamplainPetClinicApp.Services.NetworkServices;
using ChamplainPetClinicApp.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ChamplainPetClinicApp.ViewModels {
    public class LoginPageViewModel : ViewModelBase {

        private INetworkService<HttpResponseMessage> networkService;

        private string email;

        public string Email {
            get => email;
            set => SetProperty(ref email, value);
        }

        private string password;

        public string Password {
            get => password;
            set => SetProperty(ref password, value);
        }

        private bool inputsEnabled;

        public bool InputsEnabled {
            get => inputsEnabled;
            set => SetProperty(ref inputsEnabled, value);
        }

        public AsyncCommand AttemptLoginCommand { get; }

        public LoginPageViewModel() {
            InputsEnabled = true;
            Email = "";
            Password = "";

            AttemptLoginCommand = new AsyncCommand(AttemptLogin);

            networkService = NetworkService.Instance;
        }

        private async Task AttemptLogin() {
            InputsEnabled = false;
            bool authenticated = await networkService.LoginAsync(ApiConstants.GetLoginUri(), new User(Email, Password));
            InputsEnabled = true;

            if(authenticated) {
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            } else {
                await Application.Current.MainPage.DisplayAlert("Login failed!", "Your email or password is invalid. Please try again.", "OK");
            }
        }

    }
}
