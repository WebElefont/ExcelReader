using ExcelReader.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReader.Services
{
    public class AddCartrigesService
    {
        private readonly ConfigureImagesService _addImagesService;
        private readonly ConfigureProducersService _addProducersService;

        private readonly CartrigesApiService _cartrigesApiService;

        public AddCartrigesService(ConfigureImagesService addImagesService, ConfigureProducersService addProducersService, CartrigesApiService cartrigesApiService)
        {
            _addImagesService = addImagesService;
            _addProducersService = addProducersService;
            _cartrigesApiService = cartrigesApiService;
        }

        public void SendToApi(IEnumerable<CartrigeAndVaporizerCE> cartriges)
        {
            AddSpiralTypes(cartriges);
            AddCartrigeCapacities(cartriges);
            AddResistances(cartriges);

            _addImagesService.ConfigureImages(cartriges);
            _addProducersService.ConfigureProducers(cartriges);
            foreach (var cartrige in cartriges)
            {
                if (cartrige.CartrigeCapacity == 0)
                {
                    cartrige.CartrigeCapacity = null;
                }
                _cartrigesApiService.AddGood(cartrige);
            }
        }

        public void AddSpiralTypes(IEnumerable<CartrigeAndVaporizerCE> cartriges)
        {
            IEnumerable<string> spiralTypes = cartriges
                .Select(x => x.SpiralType)
                .Where(x => x != null)
                .Distinct();

            foreach (var item in spiralTypes)
                _cartrigesApiService.AddSpiralType(item);
        }

        public void AddCartrigeCapacities(IEnumerable<CartrigeAndVaporizerCE> cartriges)
        {
            IEnumerable<double> items = cartriges
                .Select(x => x.CartrigeCapacity)
                .OfType<double>()
                .Where(x => x != 0)
                .Distinct();

            foreach (var item in items)
                _cartrigesApiService.AddCartrigeCapacity(item);
        }

        public void AddResistances(IEnumerable<CartrigeAndVaporizerCE> cartriges)
        {
            IEnumerable<double> items = cartriges
                .Select(x => x.Resistance)
                .Distinct();

            foreach (var item in items)
                _cartrigesApiService.AddResistance(item);
        }
    }
}
