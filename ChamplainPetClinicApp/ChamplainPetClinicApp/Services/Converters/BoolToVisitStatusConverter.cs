using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ChamplainPetClinicApp.Services.Converters {
    public class BoolToVisitStatusConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (bool)value ? "Not Cancelled" : "Cancelled";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if((string)value == "Not Cancelled") {
                return true;
            }

            return false;
        }
    }
}
