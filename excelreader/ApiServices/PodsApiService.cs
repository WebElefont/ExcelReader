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
    public class PodsApiService
    {
        private readonly ApiService _service;

        public PodsApiService(ApiService apiService)
        {
            _service = apiService;
        }

        public void AddGood(PodCE pod)
        {
            JsonContent content = JsonContent.Create(pod);
            _service.SendRequest($"/api/Pods", HttpMethod.Post, content);
        }

        #region Weight
        public IEnumerable<double> GetWeight()
        {
            var response = _service.SendRequest($"/api/Pods/Weight", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<double>>().Result;
        }

        public void AddWeight(double weight)
        {
            _service.SendRequest($"/api/Pods/Weight/{JsonSerializer.Serialize(weight)}", HttpMethod.Post);
        }

        public void RemoveWeight(double weight)
        {
            _service.SendRequest($"/api/Pods/Weight/{JsonSerializer.Serialize(weight)}", HttpMethod.Delete);
        }
        #endregion
        #region Material
        public IEnumerable<string> GetMaterial()
        {
            var response = _service.SendRequest($"/api/Pods/Material", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result;
        }

        public void AddMaterial(string material)
        {
            _service.SendRequest($"/api/Pods/Material/{material}", HttpMethod.Post);
        }

        public void RemoveMaterial(string material)
        {
            _service.SendRequest($"/api/Pods/Material/{material}", HttpMethod.Delete);
        }
        #endregion
        #region EvaporatorResistance
        public IEnumerable<double> GetEvaporatorResistance()
        {
            var response = _service.SendRequest($"/api/Pods/EvaporatorResistance", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<double>>().Result;
        }
        public void AddEvaporatorResistance(double evaporatorResistance)
        {
            _service.SendRequest($"/api/Pods/EvaporatorResistance/{JsonSerializer.Serialize(evaporatorResistance)}", HttpMethod.Post);
        }

        public void RemoveEvaporatorResistance(double evaporatorResistance)
        {
            _service.SendRequest($"/api/Pods/EvaporatorResistance/{JsonSerializer.Serialize(evaporatorResistance)}", HttpMethod.Delete);
        }
        #endregion
        #region Power
        public IEnumerable<string> GetPower()
        {
            var response = _service.SendRequest($"/api/Pods/Power", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result;
        }

        public void AddPower(string power)
        {
            _service.SendRequest($"/api/Pods/Power/{power}", HttpMethod.Post);
        }

        public void RemovePower(string power)
        {
            _service.SendRequest($"/api/Pods/Power/{power}", HttpMethod.Delete);
        }
        #endregion
        #region Battarey
        public IEnumerable<short> GetBattarey()
        {
            var response = _service.SendRequest($"/api/Pods/Battarey", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<short>>().Result;
        }

        public void AddBattarey(short battarey)
        {
            _service.SendRequest($"/api/Pods/Battarey/{battarey}", HttpMethod.Post);
        }

        public void RemoveBattarey(short battarey)
        {
            _service.SendRequest($"/api/Pods/Battarey/{battarey}", HttpMethod.Delete);
        }
        #endregion
        #region CartrigeCapacity
        public IEnumerable<double> GetCartrigeCapacity()
        {
            var response = _service.SendRequest($"/api/Pods/CartrigeCapacity", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<double>>().Result;
        }

        public void AddCartrigeCapacity(double cartrigeCapacity)
        {
            _service.SendRequest($"/api/Pods/CartrigeCapacity/{JsonSerializer.Serialize(cartrigeCapacity)}", HttpMethod.Post);
        }

        public void RemoveCartrigeCapacity(double cartrigeCapacity)
        {
            _service.SendRequest($"/api/Pods/CartrigeCapacity/{JsonSerializer.Serialize(cartrigeCapacity)}", HttpMethod.Delete);
        }
        #endregion
        #region Port
        public IEnumerable<string> GetPort()
        {
            var response = _service.SendRequest($"/api/Pods/Port", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result;
        }

        public void AddPort(string port)
        {
            _service.SendRequest($"/api/Pods/Port/{port}", HttpMethod.Post);
        }

        public void RemovePort(string port)
        {
            _service.SendRequest($"/api/Pods/Port/{port}", HttpMethod.Delete);
        }
        #endregion
    }
}
