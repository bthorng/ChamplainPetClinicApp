using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ChamplainPetClinicApp.Services.Converters {
    public class IntToPetImageConverter : IValueConverter {

        public readonly string cat = "icon_cat.png";
        public readonly string dog = "icon_dog.png";
        public readonly string lizard = "icon_lizard.png";
        public readonly string snake = "icon_snake.png";
        public readonly string bird = "icon_bird.png";
        public readonly string hamster = "icon_hamster.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            int petType = (int)value;

            switch(petType) {
                case 1:
                    return cat;
                case 2:
                    return dog;
                case 3:
                    return lizard;
                case 4:
                    return snake;
                case 5:
                    return bird;
                case 6:
                    return hamster;
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

    }
}
