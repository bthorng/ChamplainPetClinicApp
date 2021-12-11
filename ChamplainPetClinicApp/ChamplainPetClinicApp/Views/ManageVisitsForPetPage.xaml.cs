using ChamplainPetClinicApp.Services.NetworkServices;
using ChamplainPetClinicApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChamplainPetClinicApp.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(PetId), nameof(PetId))]
    [QueryProperty(nameof(PetName), nameof(PetName))]
    public partial class ManageVisitsForPetPage : ContentPage {

        public string PetId { get; set; }
        public string PetName { get; set; }

        public ManageVisitsForPetPage() {
            InitializeComponent();
        }

        protected override async void OnAppearing() {
            base.OnAppearing();
            if(BindingContext == null) {
                BindingContext = new ManageVisitsForPetPageViewModel(PetName, PetId);
            } else {
                ManageVisitsForPetPageViewModel existingViewModel = (ManageVisitsForPetPageViewModel)BindingContext;
                await existingViewModel.RetrieveVisits();
            }
        }
    }
}