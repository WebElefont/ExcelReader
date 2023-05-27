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
    public class HookahTobaccoApiService
    {
        private readonly ApiService _service;

        public HookahTobaccoApiService(ApiService apiService)
        {
            _service = apiService;
        }

        public void AddGood(HookahTobaccoCE tobacco)
        {
            JsonContent content = JsonContent.Create(tobacco);
            _service.SendRequest($"/api/HookahTobacco", HttpMethod.Post, content);
        }

        #region Taste
        public IEnumerable<string> GetTaste()
        {
            var response = _service.SendRequest($"/api/HookahTobacco/Taste", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result;
        }
        public void AddTaste(string taste)
        {
            _service.SendRequest($"/api/HookahTobacco/Taste/{taste}", HttpMethod.Post);
        }
        public void RemoveTaste(string taste)
        {
            _service.SendRequest($"/api/HookahTobacco/Taste/{taste}", HttpMethod.Delete);
        }
        #endregion
        #region Weight
        public IEnumerable<double> GetWeight()
        {
            var response = _service.SendRequest($"/api/HookahTobacco/Weight", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<double>>().Result;
        }

        public void AddWeight(double weight)
        {
            _service.SendRequest($"/api/HookahTobacco/Weight/{JsonSerializer.Serialize(weight)}", HttpMethod.Post);
        }

        public void RemoveWeight(double weight)
        {
            _service.SendRequest($"/api/HookahTobacco/Weight/{JsonSerializer.Serialize(weight)}", HttpMethod.Delete);
        }
        #endregion
    }
}
