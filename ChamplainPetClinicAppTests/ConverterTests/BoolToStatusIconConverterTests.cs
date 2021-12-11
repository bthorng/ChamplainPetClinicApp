using ChamplainPetClinicApp.Services.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChamplainPetClinicAppTests.ConverterTests {
    public class BoolToStatusIconConverterTests {

        [Fact]
        public void Convert_returns_active_icon_when_cancelled_is_true() {

            var converter = new BoolToStatusIconConverter();

            var result = converter.Convert(true, null, null, null);

            Assert.Equal("icon_activestatus.png", (string)result);
        }

        [Fact]
        public void Convert_returns_inactive_icon_when_cancelled_is_false() {

            var converter = new BoolToStatusIconConverter();

            var result = converter.Convert(false, null, null, null);

            Assert.Equal("icon_inactivestatus.png", (string)result);
        }

    }
}
