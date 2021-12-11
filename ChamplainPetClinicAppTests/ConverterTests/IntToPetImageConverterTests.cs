using ChamplainPetClinicApp.Services.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChamplainPetClinicAppTests.ConverterTests {
    public class IntToPetImageConverterTests {

        [Fact]
        public void Convert_returns_cat_when_value_1() {

            var converter = new IntToPetImageConverter();

            var result = converter.Convert(1, null, null, null);

            Assert.Equal("icon_cat.png", (string)result);
        }

        [Fact]
        public void Convert_returns_cat_when_value_2() {

            var converter = new IntToPetImageConverter();

            var result = converter.Convert(2, null, null, null);

            Assert.Equal("icon_dog.png", (string)result);
        }

        [Fact]
        public void Convert_returns_cat_when_value_3() {

            var converter = new IntToPetImageConverter();

            var result = converter.Convert(3, null, null, null);

            Assert.Equal("icon_lizard.png", (string)result);
        }

        [Fact]
        public void Convert_returns_cat_when_value_4() {

            var converter = new IntToPetImageConverter();

            var result = converter.Convert(4, null, null, null);

            Assert.Equal("icon_snake.png", (string)result);
        }

        [Fact]
        public void Convert_returns_cat_when_value_5() {

            var converter = new IntToPetImageConverter();

            var result = converter.Convert(5, null, null, null);

            Assert.Equal("icon_bird.png", (string)result);
        }

        [Fact]
        public void Convert_returns_cat_when_value_6() {

            var converter = new IntToPetImageConverter();

            var result = converter.Convert(6, null, null, null);

            Assert.Equal("icon_hamster.png", (string)result);
        }

        [Fact]
        public void Convert_returns_empty_when_value_7() {

            var converter = new IntToPetImageConverter();

            var result = converter.Convert(7, null, null, null);

            Assert.Equal("", (string)result);
        }

    }
}
