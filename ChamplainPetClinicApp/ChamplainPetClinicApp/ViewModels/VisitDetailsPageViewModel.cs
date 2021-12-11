using ChamplainPetClinicApp.Models;
using ChamplainPetClinicApp.Services.NetworkServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ChamplainPetClinicApp.ViewModels {
    public class VisitDetailsPageViewModel : ViewModelBase {

        private INetworkService<HttpResponseMessage> networkService;

        public ObservableRangeCollection<Veterinarian> Veterinarians { get; set; }

        public Veterinarian SelectedVet { get; set; }

        public int PickerIndex { get; set; }

        public Visit LastVisitInformation { get; set; }

        public string VisitId { get; set; }
        public int PetId { get; set; }
        public int PractitionerId { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public bool IsFormInEditMode { get; set; }

        public AsyncCommand SwitchToEditFormCommand { get; }
        public AsyncCommand CancelEditingVisitCommand { get; }
        public AsyncCommand SubmitUpdatedVisitCommand { get; }

        public VisitDetailsPageViewModel(Visit visit, Veterinarian vet) {
            Title = "";
            networkService = NetworkService.Instance;

            LastVisitInformation = visit;
            VisitId = visit.visitId;
            PetId = visit.petId;
            PractitionerId = visit.practitionerId;
            Date = visit.date;
            Description = visit.description;
            Status = visit.status;

            IsFormInEditMode = false;

            SwitchToEditFormCommand = new AsyncCommand(SwitchToEditForm);
            CancelEditingVisitCommand = new AsyncCommand(CancelEditingVisit);
            SubmitUpdatedVisitCommand = new AsyncCommand(SubmitUpdatedVisit);

            Veterinarians = new ObservableRangeCollection<Veterinarian>();
            PopulatePicker();
        }

        private async Task SubmitUpdatedVisit() {
            string[] dateParts = Date.Substring(0, 10).Split('/');
            string properDate = $"{dateParts[2]}-{dateParts[0]}-{dateParts[1]}";

            Visit updatedVisit = new Visit(VisitId, properDate, Description, PetId, SelectedVet.vetId, Status);

            Visit response = await networkService.UpdateSingleAsync<Visit>(ApiConstants.GetUpdateVisitUri("" + PetId, VisitId), updatedVisit);
            
            if(response != null) {
                LastVisitInformation = response;
                await CancelEditingVisit();
            } else {
                await Application.Current.MainPage.DisplayAlert("Failed to update visit!", "Could not updated visit. Please try again.", "OK");
            }
        }

        private async Task SwitchToEditForm() {
            IsFormInEditMode = true;
            OnPropertyChanged("IsFormInEditMode");
        }

        private async Task CancelEditingVisit() {
            IsFormInEditMode = false;
            OnPropertyChanged("IsFormInEditMode");
            SetSelection();
        }

        private async void PopulatePicker() {
            List<Veterinarian> vets = await networkService.GetAsync<Veterinarian>(ApiConstants.GetVetsUri(0));

            foreach(Veterinarian vet in vets) {
                if(vet.isActive == 1) {
                    Veterinarians.Add(vet);
                }
            }

            SetSelection();
        }

        private void SetSelection() {
            Veterinarian vet = Veterinarians.Where(x => x.vetId == LastVisitInformation.practitionerId).FirstOrDefault();

            if(vet == null) return;

            PickerIndex = Veterinarians.IndexOf(vet);
            OnPropertyChanged("PickerIndex");
        }
    }
}
