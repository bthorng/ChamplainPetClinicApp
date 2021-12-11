using ChamplainPetClinicApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ChamplainPetClinicApp.Services.Converters {
    public class PetListToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            List<Pet> genericList = (value as IEnumerable<object>).Cast<Pet>().ToList();

            if(genericList.Count == 0) {
                return "";
            }

            string outputString = "Pets: ";
            for(int i = 0; i < genericList.Count; i++) {
                if(i == 10) break;

                outputString += genericList[i].name;

                if(i < genericList.Count - 1) {
                    outputString += ", ";
                }
            }

            return outputString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
