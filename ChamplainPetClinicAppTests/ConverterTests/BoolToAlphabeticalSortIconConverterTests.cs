using ChamplainPetClinicApp.Services.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChamplainPetClinicAppTests.ConverterTests {
    public class BoolToAlphabeticalSortIconConverterTests {

        [Fact]
        public void Convert_returns_sorted_down_when_bool_is_true() {

            var converter = new BoolToAlphabeticalSortIcon();

            var result = converter.Convert(true, null, null, null);

            Assert.Equal("icon_sortalphadown.png", (string)result);
        }

        [Fact]
        public void Convert_returns_sorted_up_when_bool_is_false() {

            var converter = new BoolToAlphabeticalSortIcon();

            var result = converter.Convert(false, null, null, null);

            Assert.Equal("icon_sortalphaup.png", (string)result);
        }

    }
}
