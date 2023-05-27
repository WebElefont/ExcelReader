using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using SMOKYICESHOP_API_TEST.ConnectionEntities;
using System.Text.Json;
using System.Collections;

namespace ExcelReader.ApiServices
{
    public class ApiService
    {
        private readonly HttpClient _client;

        private AuthenticationCE tokens;
        private DateTime _tokenCreationTime;

        public ApiService(HttpClient client)
        {
            _client = client;
        }

        public HttpResponseMessage SendRequest(string path, HttpMethod method, HttpContent content)
        {
            HttpRequestMessage message = new HttpRequestMessage(method, path);
            message.Content = content;

            return SendRequest(message);
        }

        public HttpResponseMessage SendRequest(string path, HttpMethod method)
        {
            HttpRequestMessage message = new HttpRequestMessage(method, path);
            return SendRequest(message);
        }

        public HttpResponseMessage SendRequest(HttpRequestMessage request)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            HttpResponseMessage response = _client.Send(request);

            response.EnsureSuccessStatusCode();
            
            return response;
        }

        public string Token
        {
            get
            {
                if (_tokenCreationTime.AddMinutes(14) < DateTime.Now)
                {
                    RefreshToken();
                }

                return tokens.AccessToken;
            }
        }

        public bool Login(string phoneNumber, string password)
        {
            LoginCE login = new LoginCE();
            login.PhoneNumber = phoneNumber;
            login.Password = password;

            try
            {
                JsonContent content = JsonContent.Create(login);
                HttpResponseMessage httpResponseMessage = _client.PostAsync("/api/User/login", content).Result;
                httpResponseMessage.EnsureSuccessStatusCode();
                tokens = httpResponseMessage.Content.ReadFromJsonAsync<AuthenticationCE>().Result;
                _tokenCreationTime = DateTime.Now;
                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        private void RefreshToken()
        {
            JsonContent content = JsonContent.Create(tokens);

            HttpResponseMessage httpResponseMessage = _client.PostAsync("/api/User/refresh", content).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            tokens = httpResponseMessage.Content.ReadFromJsonAsync<AuthenticationCE>().Result;
            _tokenCreationTime = DateTime.Now;
        }
    }
}
