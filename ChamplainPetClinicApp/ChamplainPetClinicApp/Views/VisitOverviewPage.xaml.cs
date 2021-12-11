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
    public partial class VisitOverviewPage : ContentPage {
        public VisitOverviewPage() {
            InitializeComponent();
        }

        protected override async void OnAppearing() {
            base.OnAppearing();
            if(BindingContext == null) {
                BindingContext = new VisitOverviewPageViewModel();
            } else {
                VisitOverviewPageViewModel existingViewModel = (VisitOverviewPageViewModel)BindingContext;
                await existingViewModel.FetchAllVisits();
            }
        }
    }
}