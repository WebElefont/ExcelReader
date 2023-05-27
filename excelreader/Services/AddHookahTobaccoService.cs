using ExcelReader.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace ExcelReader.Services
{
    public class AddHookahTobaccoService
    {
        private readonly ConfigureImagesService _addImagesService;
        private readonly ConfigureProducersService _addProducersService;

        private readonly HookahTobaccoApiService _hookahTobaccoApiService;

        public AddHookahTobaccoService(ConfigureImagesService addImagesService, ConfigureProducersService addProducersService, HookahTobaccoApiService hookahTobaccoApiService)
        {
            _hookahTobaccoApiService = hookahTobaccoApiService;
            _addImagesService = addImagesService;
            _addProducersService = addProducersService;
        }

        public void SendToApi(IEnumerable<HookahTobaccoCE> hookahTobaccos)
        {
            AddWeights(hookahTobaccos);
            AddTastes(hookahTobaccos);

            _addImagesService.ConfigureImages(hookahTobaccos);
            _addProducersService.ConfigureProducers(hookahTobaccos);

            foreach(var tobacco in hookahTobaccos)
            {
                _hookahTobaccoApiService.AddGood(tobacco);
            }
        }

        public void AddWeights(IEnumerable<HookahTobaccoCE> hookahTobaccos)
        {
            IEnumerable<double> weights = hookahTobaccos
                .Select(x => x.Weight)
                .OfType<double>()
                .Where(x => x != 0)
                .Distinct();

            foreach (var item in weights)
            {
                _hookahTobaccoApiService.AddWeight(item);
            }

        }

        public void AddTastes(IEnumerable<HookahTobaccoCE> hookahTobaccos)
        {
            IEnumerable<string> tastes = hookahTobaccos
                .Select(x => x.Taste)
                .Where(x => x != null)
                .Distinct();

            foreach (var item in tastes)
            {
                _hookahTobaccoApiService.AddTaste(item);
            }
        }
    }
}
