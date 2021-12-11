using ChamplainPetClinicApp.Models;
using ChamplainPetClinicApp.Services.NetworkServices;
using ChamplainPetClinicApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChamplainPetClinicApp.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(VisitId), nameof(VisitId))]
    public partial class VisitDetailsPage : ContentPage {

        public string VisitId { get; set; }

        private INetworkService<HttpResponseMessage> networkService;

        public VisitDetailsPage() {
            InitializeComponent();
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            networkService = NetworkService.Instance;

            Visit visit = await networkService.GetSingleAsync<Visit>(ApiConstants.GetVisitByIdUri(VisitId));

            Veterinarian vet = await networkService.GetSingleAsync<Veterinarian>(ApiConstants.GetVetsUri(visit.practitionerId));

            if(visit != null && vet != null) {
                BindingContext = new VisitDetailsPageViewModel(visit, vet);
            }
        }
    }
}