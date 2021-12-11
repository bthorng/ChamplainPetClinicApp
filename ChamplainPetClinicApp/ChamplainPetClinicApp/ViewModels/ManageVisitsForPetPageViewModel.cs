using ChamplainPetClinicApp.Models;
using ChamplainPetClinicApp.Services.NetworkServices;
using ChamplainPetClinicApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ChamplainPetClinicApp.ViewModels {
    public class ManageVisitsForPetPageViewModel : ViewModelBase {

        private INetworkService<HttpResponseMessage> networkService;

        public ObservableRangeCollection<Visit> ScheduledVisits { get; set; }
        public ObservableRangeCollection<Visit> PreviousVisits { get; set; }

        public string PetName { get; set; }
        public string PetId { get; set; }

        private Visit selectedVisit;

        public Visit SelectedVisit {
            get => selectedVisit;
            set => SetProperty(ref selectedVisit, value);
        }

        private Visit selectedPastVisit;

        public Visit SelectedPastVisit {
            get => selectedPastVisit;
            set => SetProperty(ref selectedPastVisit, value);
        }

        public ObservableCollection<MyCarouselPage> CarouselPages { get; set; }

        public AsyncCommand<Visit> DeleteVisitCommand { get; }
        public AsyncCommand<Visit> UpdateVisitStatusCommand { get; }
        public AsyncCommand ViewVisitDetailsCommand { get; }
        public AsyncCommand ViewPastVisitDetailsCommand { get; }
        public AsyncCommand NavigateToAddVisitPageCommand { get; }
        public AsyncCommand RefreshCommand { get; }

        public ManageVisitsForPetPageViewModel(string petName, string petId) {
            Title = $"Manage visits for {petName}";

            CarouselPages = new ObservableCollection<MyCarouselPage>();
            CarouselPages.Add(new MyCarouselPage { Page = 1 });
            CarouselPages.Add(new MyCarouselPage { Page = 2 });

            DeleteVisitCommand = new AsyncCommand<Visit>(DeleteVisit);
            UpdateVisitStatusCommand = new AsyncCommand<Visit>(UpdateVisit);
            ViewVisitDetailsCommand = new AsyncCommand(ViewVisitDetails);
            ViewPastVisitDetailsCommand = new AsyncCommand(ViewPastVisitDetails);
            NavigateToAddVisitPageCommand = new AsyncCommand(NavigateToAddVisitPage);
            RefreshCommand = new AsyncCommand(RetrieveVisits);

            PetName = petName;
            PetId = petId;

            networkService = NetworkService.Instance;
            RetrieveVisits();
        }

        private async Task NavigateToAddVisitPage() {
            var route = $"{nameof(AddVisitPage)}?PetId={PetId}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task ViewVisitDetails() {
            if(SelectedVisit == null) return;

            var route = $"{nameof(VisitDetailsPage)}?VisitId={SelectedVisit.visitId}";
            await Shell.Current.GoToAsync(route);

            SelectedVisit = null;
        }

        private async Task ViewPastVisitDetails() {
            if(SelectedPastVisit == null) return;

            var route = $"{nameof(VisitDetailsPage)}?VisitId={SelectedPastVisit.visitId}";
            await Shell.Current.GoToAsync(route);

            SelectedPastVisit = null;
        }

        private async Task UpdateVisit(Visit visit) {
            visit.status = !visit.status;

            Visit response = await networkService.UpdateSingleAsync<Visit>(ApiConstants.GetUpdateVisitUri(PetId, visit.visitId), visit);

            if(response != null) {
                await RetrieveVisits();
            }
        }

        private async Task DeleteVisit(Visit visit) {
            bool successful = await networkService.DeleteSingleAsync(ApiConstants.GetDeleteVisitUri(visit.visitId));

            if(successful) {
                await RetrieveVisits();
            } else {
                await Application.Current.MainPage.DisplayAlert("Failed to delete visit!", "Could not delete visit. Please try again.", "OK");
            }
        }

        public async Task RetrieveVisits() {
            IsBusy = true;
            var scheduledVisits = await networkService.GetAsync<Visit>(ApiConstants.GetVisitsForPetUri(PetId, true));
            var previousVisits = await networkService.GetAsync<Visit>(ApiConstants.GetVisitsForPetUri(PetId, false));
            IsBusy = false;

            var sortedScheduled = scheduledVisits.OrderBy(v => v.date).Reverse();
            var sortedPrevious = previousVisits.OrderBy(v => v.date).Reverse();

            ScheduledVisits = new ObservableRangeCollection<Visit>(sortedScheduled);
            PreviousVisits = new ObservableRangeCollection<Visit>(sortedPrevious);

            OnPropertyChanged("ScheduledVisits");
            OnPropertyChanged("PreviousVisits");

            SelectedVisit = null;
        }

    }
}
