using ChamplainPetClinicApp.Models;
using ChamplainPetClinicApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ChamplainPetClinicApp.ViewModels {
    public class VeterinarianProfilePageViewModel : ViewModelBase {

        private INetworkService<HttpResponseMessage> networkService;
        private int VetId;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Resume { get; set; }
        public List<Specialty> Specialties { get; set; }
        public string Workdays { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }

        public AsyncCommand RefreshCommand { get; }

        public VeterinarianProfilePageViewModel(int vetId) {
            Title = "Veterinarian Profile";
            VetId = vetId;

            RefreshCommand = new AsyncCommand(LoadVetInformation);

            networkService = NetworkService.Instance;
            Specialties = new List<Specialty>();
            LoadVetInformation();
        }

        private async Task LoadVetInformation() {
            IsBusy = true;
            var vet = await networkService.GetSingleAsync<Veterinarian>(ApiConstants.GetVetsUri(VetId));
            IsBusy = false;

            if(vet != null) {
                FirstName = vet.firstName;
                LastName = vet.lastName;
                Resume = vet.resume;
                Workdays = vet.workday;
                Email = vet.email;
                Telephone = vet.phoneNumber;

                Specialties = vet.specialties;
            } else {
                FirstName = "Could not load first name";
                LastName = "Could not load last name";
                Resume = "Could not load resume";
                Workdays = "Could not load workdays";
                Email = "Could not load email";
                Telephone = "Could not load telephone";
            }

            OnPropertyChanged("FirstName");
            OnPropertyChanged("LastName");
            OnPropertyChanged("Resume");
            OnPropertyChanged("Workdays");
            OnPropertyChanged("Email");
            OnPropertyChanged("Telephone");
            OnPropertyChanged("Specialties");
        }
    }
}
