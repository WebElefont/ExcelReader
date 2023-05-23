using ExcelReader.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReader.Services
{
    public class AddGoodsFacade
    {
        private readonly AddCartrigesService _cartrigesService;
        private readonly AddLiquidsService _liquidsService;
        private readonly AddPodsService _podsService;
        public AddGoodsFacade()
        {
            HttpClient httpClient = new HttpClient();
            //httpClient.BaseAddress = new Uri("https://smokyiceshopapitest20230411200906.azurewebsites.net");
            httpClient.BaseAddress = new Uri("http://localhost:5171");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            ApiService apiService = new ApiService(httpClient);
            CartrigesApiService cartrigesApiService = new CartrigesApiService(apiService);
            ImagesApiService imagesApiService = new ImagesApiService(apiService);
            LiquidsApiService liquidsApiService = new LiquidsApiService(apiService);
            ProducersApiService producersApiService = new ProducersApiService(apiService);
            PodsApiService podsApiService = new PodsApiService(apiService);
            ConfigureImagesService addImagesService = new ConfigureImagesService(imagesApiService);
            ConfigureProducersService addProducersService = new ConfigureProducersService(producersApiService);

            _cartrigesService = new AddCartrigesService(addImagesService, addProducersService, cartrigesApiService);
            _liquidsService = new AddLiquidsService(addImagesService, addProducersService, liquidsApiService);
            _podsService = new AddPodsService(addImagesService, addProducersService, podsApiService);
            apiService.Login("+380964873560", "1234");
        }

        public void SendLiquids(IEnumerable<LiquidCE> liquids) => _liquidsService.SendToApi(liquids); 
        public void SendCartriges(IEnumerable<CartrigeAndVaporizerCE> cartriges) => _cartrigesService.SendToApi(cartriges);
        public void SendPods(IEnumerable<PodCE> pods) => _podsService.SendToApi(pods);

    }
}
