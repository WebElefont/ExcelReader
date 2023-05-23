using ExcelReader.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReader.ApiServices
{
    public class ProducersApiService
    {
        private readonly ApiService _service;

        public ProducersApiService(ApiService apiService)
        {
            _service = apiService;
        }

        public void AddProducer(ProducerCE producer)
        {
            JsonContent content = JsonContent.Create(producer);
            _service.SendRequest($"/api/Producers", HttpMethod.Post, content);
        }

        public IEnumerable<ProducerCE> GetProducers()
        {
            var response = _service.SendRequest($"/api/Producers", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<ProducerCE>>().Result;
        }
    }
}
