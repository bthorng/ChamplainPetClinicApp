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
    public class OwnerProfilePageViewModel : ViewModelBase {

        private INetworkService<HttpResponseMessage> networkService;
        private int OwnerId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public List<Pet> Pets { get; set; }

        public AsyncCommand<Pet> ManageVisitsForPetCommand { get; }
        public AsyncCommand RefreshCommand { get; }

        public OwnerProfilePageViewModel(string ownerId) {
            ManageVisitsForPetCommand = new AsyncCommand<Pet>(ManageVisitsForPet);
            RefreshCommand = new AsyncCommand(LoadOwnerInformation);

            OwnerId = int.Parse(ownerId);
            Telephone = "0000000000";

            networkService = NetworkService.Instance;
            LoadOwnerInformation();
        }

        private async Task LoadOwnerInformation() {
            IsBusy = true;
            var owner = await networkService.GetSingleAsync<Owner>(ApiConstants.GetOwnersUri(OwnerId));
            IsBusy = false;

            if(owner != null) {
                FirstName = owner.firstName;
                LastName = owner.lastName;
                Address = owner.address;
                City = owner.city;
                Telephone = owner.telephone;

                Pets = owner.pets;
            } else {
                FirstName = "Could not load first name";
                LastName = "Could not load last name";
                Address = "Could not load address";
                City = "Could not load city";
                Pets = new List<Pet>();
            }

            OnPropertyChanged("FirstName");
            OnPropertyChanged("LastName");
            OnPropertyChanged("Address");
            OnPropertyChanged("City");
            OnPropertyChanged("Telephone");
            OnPropertyChanged("Pets");
        }

        private async Task ManageVisitsForPet(Pet pet) {
            if(pet == null) return;

            var route = $"{nameof(ManageVisitsForPetPage)}?PetId={pet.id}&PetName={pet.name}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
