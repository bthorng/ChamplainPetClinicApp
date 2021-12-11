using ChamplainPetClinicApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChamplainPetClinicApp.Services {
    public class PageDataTemplateSelector : DataTemplateSelector {

        public DataTemplate Page1 { get; set; }
        public DataTemplate Page2 { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
            switch(((MyCarouselPage)item).Page) {
                case 1:
                    return Page1;
                case 2:
                    return Page2;
                default:
                    return Page1;
            }
        }
    }
}
