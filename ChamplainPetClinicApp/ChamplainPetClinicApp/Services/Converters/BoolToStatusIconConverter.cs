using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ChamplainPetClinicApp.Services.Converters {
    public class BoolToStatusIconConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            bool cancelled = (bool)value;

            return cancelled ? "icon_activestatus.png" : "icon_inactivestatus.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
