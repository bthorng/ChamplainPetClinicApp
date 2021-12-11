using ChamplainPetClinicApp.Services.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChamplainPetClinicAppTests.ConverterTests {
    public class BoolToVisitStatusConverterTests {

        [Fact]
        public void Convert_returns_not_cancelled_when_true() {

            var converter = new BoolToVisitStatusConverter();

            var result = converter.Convert(true, null, null, null);

            Assert.Equal("Not Cancelled", (string)result);
        }

        [Fact]
        public void Convert_returns_cancelled_when_false() {

            var converter = new BoolToVisitStatusConverter();

            var result = converter.Convert(false, null, null, null);

            Assert.Equal("Cancelled", (string)result);
        }

        [Fact]
        public void ConvertBack_returns_true_when_string_is_not_cancelled() {

            var converter = new BoolToVisitStatusConverter();

            var result = converter.ConvertBack("Not Cancelled", null, null, null);

            Assert.True((bool)result);
        }

        [Fact]
        public void ConvertBack_returns_false_when_string_is_cancelled() {

            var converter = new BoolToVisitStatusConverter();

            var result = converter.ConvertBack("Cancelled", null, null, null);

            Assert.False((bool)result);
        }

    }
}
