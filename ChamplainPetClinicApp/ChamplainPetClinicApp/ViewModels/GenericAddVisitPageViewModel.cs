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
    public class GenericAddVisitPageViewModel : ViewModelBase {
        private INetworkService<HttpResponseMessage> networkService;

        public ObservableRangeCollection<Owner> Owners { get; set; }
        private Owner selectedOwner;
        public Owner SelectedOwner {
            get => selectedOwner;
            set {
                SetProperty(ref selectedOwner, value);
                PopulatePetsPicker();
            }
        }

        public ObservableRangeCollection<Pet> Pets { get; set; }
        public Pet SelectedPet { get; set; }

        public ObservableRangeCollection<Veterinarian> Veterinarians { get; set; }
        public Veterinarian SelectedVeterinarian { get; set; }

        public string Date { get; set; }
        public string Description { get; set; }

        public string MinimumDate { get; set; }

        private bool petsPickerPopulated;

        public bool PetsPickerPopulated {
            get => petsPickerPopulated;
            set => SetProperty(ref petsPickerPopulated, value);
        }

        public AsyncCommand CreateVisitCommand { get; }

        public GenericAddVisitPageViewModel() {
            Title = "Create a new visit";
            MinimumDate = DateTime.Now.ToString();

            CreateVisitCommand = new AsyncCommand(CreateVisit);

            networkService = NetworkService.Instance;

            Veterinarians = new ObservableRangeCollection<Veterinarian>();
            Pets = new ObservableRangeCollection<Pet>();
            PetsPickerPopulated = false;
            PopulateOwnersPicker();
            PopulateVeterinariansPicker();
        }

        private async Task CreateVisit() {
            if(
                SelectedOwner == null ||
                SelectedPet == null ||
                SelectedVeterinarian == null ||
                string.IsNullOrWhiteSpace(Date) ||
                string.IsNullOrWhiteSpace(Description)) {
                await Application.Current.MainPage.DisplayAlert("Required fields are not filled!", "Please fill out all fields.", "OK");
                return;
            }

            string[] dateParts = Date.Substring(0, 10).Split('/');
            string properDate = $"{dateParts[2]}-{dateParts[0]}-{dateParts[1]}";

            Visit createdVisit = new Visit("", properDate, Description, SelectedPet.id, SelectedVeterinarian.vetId, true);

            Visit response = await networkService.PostSingleAsync<Visit>(ApiConstants.GetCreateVisitUri("" + SelectedPet.id), createdVisit);

            if(response != null) {
                await Shell.Current.GoToAsync("..");
            }
        }

        private async void PopulateVeterinariansPicker() {
            List<Veterinarian> vets = await networkService.GetAsync<Veterinarian>(ApiConstants.GetVetsUri(0));

            foreach(Veterinarian vet in vets) {
                if(vet.isActive == 1) {
                    Veterinarians.Add(vet);
                }
            }
        }

        private async void PopulateOwnersPicker() {
            var owners = await networkService.GetAsync<Owner>(ApiConstants.GetOwnersUri(0));

            Owners = new ObservableRangeCollection<Owner>(owners);
            OnPropertyChanged("Owners");
        }

        private async void PopulatePetsPicker() {
            var owner = await networkService.GetSingleAsync<Owner>(ApiConstants.GetOwnersUri(SelectedOwner.id));

            Pets = new ObservableRangeCollection<Pet>(owner.pets);
            OnPropertyChanged("Pets");
            PetsPickerPopulated = true;
        }
    }
}
