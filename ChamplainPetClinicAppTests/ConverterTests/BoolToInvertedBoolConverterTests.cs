using ChamplainPetClinicApp.Services.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChamplainPetClinicAppTests.ConverterTests {
    public class BoolToInvertedBoolConverterTests {

        [Fact]
        public void Convert_returns_false_when_bool_is_true() {

            var converter = new BoolToInvertedBoolConverter();

            var result = converter.Convert(true, null, null, null);

            Assert.False((bool)result);
        }

        [Fact]
        public void Convert_returns_true_when_bool_is_false() {

            var converter = new BoolToInvertedBoolConverter();

            var result = converter.Convert(false, null, null, null);

            Assert.True((bool)result);
        }

    }
}
