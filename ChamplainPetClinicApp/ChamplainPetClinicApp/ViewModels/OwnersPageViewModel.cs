using ChamplainPetClinicApp.Models;
using ChamplainPetClinicApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using ChamplainPetClinicApp.Views;
using System.Net.Http;

namespace ChamplainPetClinicApp.ViewModels {
    public class OwnersPageViewModel : ViewModelBase {

        private INetworkService<HttpResponseMessage> networkService;

        public ObservableRangeCollection<Owner> Owners { get; set; }

        public AsyncCommand<Owner> ViewOwnerProfileCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand SortAlphabeticallyCommand { get; }

        public bool SortedAlphabeticallyDown { get; set; }

        public OwnersPageViewModel() {
            ViewOwnerProfileCommand = new AsyncCommand<Owner>(ViewOwnerProfile);
            RefreshCommand = new AsyncCommand(FetchOwners);
            SortAlphabeticallyCommand = new AsyncCommand(SortAlphabetically);

            networkService = NetworkService.Instance;

            FetchOwners();
        }

        private async Task SortAlphabetically() {
            SortedAlphabeticallyDown = !SortedAlphabeticallyDown;

            var reversed = Owners.Reverse();
            Owners = new ObservableRangeCollection<Owner>(reversed);

            OnPropertyChanged("SortedAlphabeticallyDown");
            OnPropertyChanged("Owners");
        }

        private async Task FetchOwners() {
            SortedAlphabeticallyDown = true;

            IsBusy = true;
            var result = await networkService.GetAsync<Owner>(ApiConstants.GetOwnersUri(0));
            IsBusy = false;
            
            if(result != null) {
                var sorted = result.OrderBy(o => o.firstName);
                Owners = new ObservableRangeCollection<Owner>(sorted);
            } else {
                Owners = new ObservableRangeCollection<Owner>();
            }

            OnPropertyChanged("SortedAlphabeticallyDown");
            OnPropertyChanged("Owners");
        }

        private async Task ViewOwnerProfile(Owner owner) {
            if(owner == null) return;

            var route = $"{nameof(OwnerProfilePage)}?OwnerId={owner.id}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
