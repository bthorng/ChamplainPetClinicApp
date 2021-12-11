using ChamplainPetClinicApp.Services.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChamplainPetClinicAppTests.ConverterTests {
    public class IntToStatusIconConverterTests {

        [Fact]
        public void Convert_returns_active_when_value_1() {

            var converter = new IntToStatusIconConverter();

            var result = converter.Convert(1, null, null, null);

            Assert.Equal("icon_activestatus.png", (string)result);
        }

        [Fact]
        public void Convert_returns_active_when_value_not_1() {

            var converter = new IntToStatusIconConverter();

            var result = converter.Convert(0, null, null, null);

            Assert.Equal("icon_inactivestatus.png", (string)result);
        }

    }
}
