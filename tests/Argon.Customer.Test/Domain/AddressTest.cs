using Argon.Core.Internationalization;
using Argon.Customers.Test.Domain.Fixtures;

namespace Argon.Customers.Test.Domain
{
    public class AddressTest
    {
        private readonly Localizer _localizer;
        private readonly AddressFixture _addressFixture;

        public AddressTest()
        {
            _localizer = Localizer.GetLocalizer();
            _addressFixture = new AddressFixture();
        }
    }
}
