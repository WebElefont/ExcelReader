using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReader.ApiServices
{
    public class ImagesApiService
    {
        private readonly ApiService _service;

        public ImagesApiService(ApiService apiService)
        {
            _service = apiService;
        }

        public Guid AddImage(string imagePath)
        {
            var content = new MultipartFormDataContent();
            FileStream stream = File.OpenRead(imagePath);
            content.Add(new StreamContent(stream), "image", imagePath);

            var response = _service.SendRequest("/api/Images", HttpMethod.Post, content); // -enenenneneeeeeeeeeeeeeenenenenenenene
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        }
    }
}
