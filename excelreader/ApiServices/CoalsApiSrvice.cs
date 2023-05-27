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
    public class CoalsApiSrvice
    {
        private readonly ApiService _service;

        public CoalsApiSrvice(ApiService apiService)
        {
            _service = apiService;
        }

        public void AddGood(CoalCE coal)
        {
            JsonContent content = JsonContent.Create(coal);
            _service.SendRequest($"/api/Coals", HttpMethod.Post, content);
        }

        #region Type
        public IEnumerable<string> GetType() 
        {
            var response = _service.SendRequest($"/api/Coals/Type", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result;
        }

        public void AddType(string type)
        {
            _service.SendRequest($"/api/Coals/Type/{type}", HttpMethod.Post);
        }

        public void RemoveType(string type)
        {
            _service.SendRequest($"/api/Coals/Type/{type}", HttpMethod.Delete);
        }
        #endregion

        #region Weight
        public IEnumerable<double> GetWeight()
        {
            var response = _service.SendRequest($"/api/Coals/Weight", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<double>>().Result;
        }

        public void AddWeight(double weight)
        {
            _service.SendRequest($"/api/Coals/Weight/{JsonSerializer.Serialize(weight)}", HttpMethod.Post);
        }

        public void RemoveWeight(double weight)
        {
            _service.SendRequest($"/api/Coals/Weight/{JsonSerializer.Serialize(weight)}", HttpMethod.Delete);
        }
        #endregion
    }
}
