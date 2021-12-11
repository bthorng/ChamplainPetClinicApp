using ChamplainPetClinicApp.Models;
using ChamplainPetClinicApp.Services.NetworkServices;
using ChamplainPetClinicApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ChamplainPetClinicApp.ViewModels {
    public class VeterinariansPageViewModel : ViewModelBase {

        private INetworkService<HttpResponseMessage> networkService;

        public ObservableRangeCollection<Veterinarian> Veterinarians { get; set; }

        public AsyncCommand<Veterinarian> ViewVetProfileCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand SortAlphabeticallyCommand { get; }

        public bool SortedAlphabeticallyDown { get; set; }

        public VeterinariansPageViewModel() {
            Title = "Veterinarians";

            ViewVetProfileCommand = new AsyncCommand<Veterinarian>(ViewVetProfile);
            RefreshCommand = new AsyncCommand(FetchVets);
            SortAlphabeticallyCommand = new AsyncCommand(SortAlphabetically);

            networkService = NetworkService.Instance;

            FetchVets();
        }

        private async Task SortAlphabetically() {
            SortedAlphabeticallyDown = !SortedAlphabeticallyDown;

            var reversed = Veterinarians.Reverse();
            Veterinarians = new ObservableRangeCollection<Veterinarian>(reversed);

            OnPropertyChanged("SortedAlphabeticallyDown");
            OnPropertyChanged("Veterinarians");
        }

        private async Task FetchVets() {
            SortedAlphabeticallyDown = true;

            IsBusy = true;
            var result = await networkService.GetAsync<Veterinarian>(ApiConstants.GetVetsUri(0));
            IsBusy = false;

            if(result != null) {
                var sorted = result.OrderBy(o => o.firstName);
                Veterinarians = new ObservableRangeCollection<Veterinarian>(sorted);
            } else {
                Veterinarians = new ObservableRangeCollection<Veterinarian>();
            }

            OnPropertyChanged("SortedAlphabeticallyDown");
            OnPropertyChanged("Veterinarians");
        }

        private async Task ViewVetProfile(Veterinarian vet) {
            if(vet == null) return;

            var route = $"{nameof(VeterinarianProfilePage)}?VetId={vet.vetId}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
