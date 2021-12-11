using ChamplainPetClinicApp.Models;
using ChamplainPetClinicApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ChamplainPetClinicApp.ViewModels {
    public class AddVisitPageViewModel : ViewModelBase {

        private INetworkService<HttpResponseMessage> networkService;

        private int PetId { get; set; }

        public ObservableRangeCollection<Veterinarian> Veterinarians { get; set; }
        public Veterinarian SelectedVeterinarian { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }

        public string MinimumDate { get; set; }

        public AsyncCommand CreateVisitCommand { get; }

        public AddVisitPageViewModel(int petId) {
            Title = "Create a new visit";
            MinimumDate = DateTime.Now.ToString(); ;

            CreateVisitCommand = new AsyncCommand(CreateVisit);

            networkService = NetworkService.Instance;
            PetId = petId;

            Veterinarians = new ObservableRangeCollection<Veterinarian>();
            PopulatePicker();
        }

        private async Task CreateVisit() {
            if(SelectedVeterinarian == null ||
               string.IsNullOrWhiteSpace(Date) ||
               string.IsNullOrWhiteSpace(Description)) {
                await Application.Current.MainPage.DisplayAlert("Required fields are not filled!", "Please fill out all fields.", "OK");
                return;
            }

            string[] dateParts = Date.Substring(0, 10).Split('/');
            string properDate = $"{dateParts[2]}-{dateParts[0]}-{dateParts[1]}";

            Visit createdVisit = new Visit("", properDate, Description, PetId, SelectedVeterinarian.vetId, true);

            Visit response = await networkService.PostSingleAsync<Visit>(ApiConstants.GetCreateVisitUri("" + PetId), createdVisit);

            if(response != null) {
                await Shell.Current.GoToAsync("..");
            }
        }

        private async void PopulatePicker() {
            List<Veterinarian> vets = await networkService.GetAsync<Veterinarian>(ApiConstants.GetVetsUri(0));

            foreach(Veterinarian vet in vets) {
                if(vet.isActive == 1) {
                    Veterinarians.Add(vet);
                }
            }
        }

    }
}
