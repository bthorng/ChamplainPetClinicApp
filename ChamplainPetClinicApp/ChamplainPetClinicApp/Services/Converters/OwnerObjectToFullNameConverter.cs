using ChamplainPetClinicApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ChamplainPetClinicApp.Services.Converters {
    public class OwnerObjectToFullNameConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Owner owner = (Owner)value;

            return owner.firstName + " " + owner.lastName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
