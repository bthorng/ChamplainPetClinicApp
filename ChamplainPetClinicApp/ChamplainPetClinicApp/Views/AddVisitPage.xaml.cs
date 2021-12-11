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
    public partial class AddVisitPage : ContentPage {

        public int PetId { get; set; }

        public AddVisitPage() {
            InitializeComponent();
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            BindingContext = new AddVisitPageViewModel(PetId);
        }
    }
}