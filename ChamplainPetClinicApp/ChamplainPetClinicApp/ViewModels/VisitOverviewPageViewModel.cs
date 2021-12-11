using ChamplainPetClinicApp.Models;
using ChamplainPetClinicApp.Services.NetworkServices;
using ChamplainPetClinicApp.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ChamplainPetClinicApp.ViewModels {
    public class VisitOverviewPageViewModel : ViewModelBase {

        private INetworkService<HttpResponseMessage> networkService;

        public ObservableRangeCollection<Visit> AllVisits { get; set; }

        private Visit selectedVisit;

        public Visit SelectedVisit {
            get => selectedVisit;
            set => SetProperty(ref selectedVisit, value);
        }

        public AsyncCommand<Visit> DeleteVisitCommand { get; }
        public AsyncCommand<Visit> UpdateVisitStatusCommand { get; }
        public AsyncCommand ViewVisitDetailsCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand NavigateToGenericAddVisitPageCommand { get; }

        public VisitOverviewPageViewModel() {
            Title = "Visit Overview";

            networkService = NetworkService.Instance;

            DeleteVisitCommand = new AsyncCommand<Visit>(DeleteVisit);
            UpdateVisitStatusCommand = new AsyncCommand<Visit>(UpdateVisit);
            ViewVisitDetailsCommand = new AsyncCommand(ViewVisitDetails);
            RefreshCommand = new AsyncCommand(FetchAllVisits);
            NavigateToGenericAddVisitPageCommand = new AsyncCommand(NavigateToGenericAddVisitPage);

            FetchAllVisits();
        }

        private async Task NavigateToGenericAddVisitPage() {
            await Shell.Current.GoToAsync($"{nameof(GenericAddVisitPage)}");
        }

        public async Task FetchAllVisits() {
            IsBusy = true;

            var owners = await networkService.GetAsync<Owner>(ApiConstants.GetOwnersUri(0));
            
            if(owners != null) {
                List<Visit> foundVisits = new List<Visit>();
                
                foreach(Owner owner in owners) {
                    foreach(Visit visit in owner.visits) {
                        foundVisits.Add(visit);
                    }
                }

                var currentDate = DateTime.Now;

                var upcomingVisits = foundVisits.Where(v => DateTime.Parse(v.date, CultureInfo.InvariantCulture) >= currentDate).OrderBy(v => DateTime.Parse(v.date, CultureInfo.InvariantCulture));
                
                AllVisits = new ObservableRangeCollection<Visit>(upcomingVisits);
            } else {
                AllVisits = new ObservableRangeCollection<Visit>();
            }

            OnPropertyChanged("AllVisits");
            IsBusy = false;
        }

        private async Task ViewVisitDetails() {
            if(SelectedVisit == null) return;

            var route = $"{nameof(VisitDetailsPage)}?VisitId={SelectedVisit.visitId}";
            await Shell.Current.GoToAsync(route);

            SelectedVisit = null;
        }

        private async Task UpdateVisit(Visit visit) {
            visit.status = !visit.status;

            Visit response = await networkService.UpdateSingleAsync<Visit>(ApiConstants.GetUpdateVisitUri("" + visit.petId, visit.visitId), visit);

            if(response != null) {
                await FetchAllVisits();
            }
        }

        private async Task DeleteVisit(Visit visit) {
            bool successful = await networkService.DeleteSingleAsync(ApiConstants.GetDeleteVisitUri(visit.visitId));

            if(successful) {
                await FetchAllVisits();
            }
        }
    }
}
