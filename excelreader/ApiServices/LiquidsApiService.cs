using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ExcelReader.Services;

namespace ExcelReader.ApiServices
{
    public class LiquidsApiService
    {
        private readonly ApiService _service;

        public LiquidsApiService(ApiService apiService)
        {
            _service = apiService;
        }

        public void AddGood(LiquidCE liquid)
        {
            JsonContent content = JsonContent.Create(liquid);
            _service.SendRequest($"/api/Liquids", HttpMethod.Post, content);
        }

        #region NicotineStrength
        public IEnumerable<byte> GetNicotineStrength()
        {
            var response = _service.SendRequest($"/api/Liquids/NicotineStrength", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<byte>>().Result;
        }
        public void AddNicotineStrength(byte strength)
        {
            _service.SendRequest($"/api/Liquids/NicotineStrength/{strength}", HttpMethod.Post);
        }
        public void RemoveNicotineStrength(byte strength)
        {
            _service.SendRequest($"/api/Liquids/NicotineStrength/{strength}", HttpMethod.Delete);
        }
        #endregion
        #region NicotineType
        public IEnumerable<string> GetNicotineType()
        {
            var response = _service.SendRequest($"/api/Liquids/NicotineType", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result;
        }
        public void AddNicotineType(string type)
        {
            _service.SendRequest($"/api/Liquids/NicotineType/{type}", HttpMethod.Post);
        }
        public void RemoveNicotineType(string type)
        {
            _service.SendRequest($"/api/Liquids/NicotineType/{type}", HttpMethod.Delete);
        }
        #endregion
        #region Capacity
        public IEnumerable<byte> GetCapacity()
        {
            var response = _service.SendRequest($"/api/Liquids/Capacity", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<byte>>().Result;
        }
        public void AddCapacity(byte capacity)
        {
            _service.SendRequest($"/api/Liquids/Capacity/{capacity}", HttpMethod.Post);
        }
        public void RemoveCapacity(byte capacity)
        {
            _service.SendRequest($"/api/Liquids/Capacity/{capacity}", HttpMethod.Delete);
        }
        #endregion
        #region TasteGroup
        public IEnumerable<string> GetTasteGroup()
        {
            var response = _service.SendRequest($"/api/Liquids/TasteGroup", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result;
        }
        public void AddTasteGroup(string tasteGroup)
        {
            _service.SendRequest($"/api/Liquids/TasteGroup/{tasteGroup}", HttpMethod.Post);
        }
        public void RemoveTasteGroup(string tasteGroup)
        {
            _service.SendRequest($"/api/Liquids/TasteGroup/{tasteGroup}", HttpMethod.Delete);
        }
        #endregion
        #region Taste
        public IEnumerable<string> GetTaste()
        {
            var response = _service.SendRequest($"/api/Liquids/Taste", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result;
        }
        public void AddTaste(string tasteGroup, string taste)
        {
            _service.SendRequest($"/api/Liquids/Taste/{tasteGroup}/{taste}", HttpMethod.Post);
        }
        public void RemoveTaste(string tasteGroup, string taste)
        {
            _service.SendRequest($"/api/Liquids/Taste/{tasteGroup}/{taste}", HttpMethod.Delete);
        }
        #endregion
    }
}
