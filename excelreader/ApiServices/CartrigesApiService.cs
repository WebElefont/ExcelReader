using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace ExcelReader.ApiServices
{
    public class CartrigesApiService
    {
        private readonly ApiService _service;

        public CartrigesApiService(ApiService apiService)
        {
            _service = apiService;
        }

        public CartrigeAndVaporizerCE AddGood(CartrigeAndVaporizerCE cartrige)
        {
            JsonContent content = JsonContent.Create(cartrige);
            HttpResponseMessage response = _service.SendRequest($"/api/CartrigesAndVaporizers", HttpMethod.Post, content);
            return response.Content.ReadFromJsonAsync<CartrigeAndVaporizerCE>().Result;
        }

        public IEnumerable<string> GetSpiralTypes()
        {
            var response = _service.SendRequest($"/api/CartrigesAndVaporizers/SpiralType", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result;
        }

        public void AddSpiralType(string spiralType)
        {
            _service.SendRequest($"/api/CartrigesAndVaporizers/SpiralType/{spiralType}", HttpMethod.Post);
        }

        public void RemoveSpiralType(string spiralType)
        {
            _service.SendRequest($"/api/CartrigesAndVaporizers/SpiralType/{spiralType}", HttpMethod.Delete);
        }

        public IEnumerable<double> GetCartrigeCapacities()
        {
            var response = _service.SendRequest($"/api/CartrigesAndVaporizers/CartrigeCapacity", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<double>>().Result;
        }

        public void AddCartrigeCapacity(double capacity)
        {
            _service.SendRequest($"/api/CartrigesAndVaporizers/CartrigeCapacity/{JsonSerializer.Serialize(capacity)}/", HttpMethod.Post);
        }

        public void RemoveCartrigeCapacity(double capacity)
        {
            _service.SendRequest($"/api/CartrigesAndVaporizers/CartrigeCapacity/{JsonSerializer.Serialize(capacity)}/", HttpMethod.Delete);
        }

        public IEnumerable<double> GetResistances()
        {
            var response = _service.SendRequest($"/api/CartrigesAndVaporizers/Resistance", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<double>>().Result;
        }

        public void AddResistance(double resistance)
        {
            _service.SendRequest($"/api/CartrigesAndVaporizers/Resistance/{JsonSerializer.Serialize(resistance)}/", HttpMethod.Post);
        }

        public void RemoveResistance(double resistance)
        {
            _service.SendRequest($"/api/CartrigesAndVaporizers/Resistance/{JsonSerializer.Serialize(resistance)}/", HttpMethod.Delete);
        }
    }
}
