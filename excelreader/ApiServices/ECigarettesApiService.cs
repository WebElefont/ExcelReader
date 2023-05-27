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
    public class ECigarettesApiService
    {
        private readonly ApiService _service;

        public ECigarettesApiService(ApiService service)
        {
            _service = service;
        }   

        public void AddGood(ECigaretteCE eCigarette)
        {
            JsonContent content = JsonContent.Create(eCigarette);
            _service.SendRequest($"/api/ECigarettes", HttpMethod.Post, content);
        }

        #region BattareyCapacity
        public IEnumerable<short> GetBattareyCapacity()
        {
            var response = _service.SendRequest($"/api/ECigarettes/BattareyCapacity", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<short>>().Result;
        }

        public void AddBattareyCapacity(short capacity)
        {
            _service.SendRequest($"/api/ECigarettes/BattareyCapacity/{capacity}", HttpMethod.Post);
        }

        public void RemoveBattareyCapacity(short capacity)
        {
            _service.SendRequest($"/api/ECigarettes/BattareyCapacity/{capacity}", HttpMethod.Delete);
        }
        #endregion
        #region Taste
        public IEnumerable<string> GetTaste()
        {
            var response = _service.SendRequest($"/api/ECigarettes/Taste", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result;
        }
        public void AddTaste(string taste)
        {
            _service.SendRequest($"/api/ECigarettes/Taste/{taste}", HttpMethod.Post);
        }
        public void RemoveTaste(string taste)
        {
            _service.SendRequest($"/api/ECigarettes/Taste/{taste}", HttpMethod.Delete);
        }
        #endregion
        #region EvaporatorVolume
        public IEnumerable<byte> GetEvaporatorVolume()
        {
            var response = _service.SendRequest($"/api/ECigarettes/EvaporatorVolume", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<byte>>().Result;
        }

        public void AddEvaporatorVolume(byte volume)
        {
            _service.SendRequest($"/api/ECigarettes/EvaporatorVolume/{volume}", HttpMethod.Post);
        }

        public void RemoveEvaporatorVolume(byte volume)
        {
            _service.SendRequest($"/api/ECigarettes/EvaporatorVolume/{volume}", HttpMethod.Delete);
        }
        #endregion
        #region PuffsCount
        public IEnumerable<int> GetPuffsCount()
        {
            var response = _service.SendRequest($"/api/ECigarettes/PuffsCount", HttpMethod.Get);
            return response.Content.ReadFromJsonAsync<IEnumerable<int>>().Result;
        }

        public void AddPuffsCount(int count)
        {
            _service.SendRequest($"/api/ECigarettes/PuffsCount/{count}", HttpMethod.Post);
        }

        public void RemovePuffsCount(int count)
        {
            _service.SendRequest($"/api/ECigarettes/PuffsCount/{count}", HttpMethod.Delete);
        }
        #endregion

    }
}
