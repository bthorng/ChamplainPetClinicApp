using ChamplainPetClinicApp.Services.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChamplainPetClinicAppTests.ConverterTests {
    public class BoolToVisitStatusButtonActionConverterTests {

        [Fact]
        public void Convert_returns_cancel_when_true() {

            var converter = new BoolToVisitStatusButtonActionConverter();

            var result = converter.Convert(true, null, null, null);

            Assert.Equal("Cancel", (string)result);
        }

        [Fact]
        public void Convert_returns_uncancel_when_false() {

            var converter = new BoolToVisitStatusButtonActionConverter();

            var result = converter.Convert(false, null, null, null);

            Assert.Equal("Uncancel", (string)result);
        }

    }
}
