using ExcelReader.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace ExcelReader.Services
{
    public class AddCoalsService
    {
        private readonly ConfigureImagesService _addImagesService;
        private readonly ConfigureProducersService _addProducersService;

        private readonly CoalsApiSrvice _coalsApiService;

        public AddCoalsService(ConfigureImagesService addImagesService, ConfigureProducersService addProducersService, CoalsApiSrvice coalsApiService)
        {
            _coalsApiService = coalsApiService;
            _addImagesService = addImagesService;
            _addProducersService = addProducersService;
        }

        public void SendToApi(IEnumerable<CoalCE> coals)
        {
            AddWeights(coals);
            AddTypes(coals);

            _addImagesService.ConfigureImages(coals);
            _addProducersService.ConfigureProducers(coals);

            foreach(var coal in coals) 
            { 
                _coalsApiService.AddGood(coal);
            }
        }

        public void AddWeights(IEnumerable<CoalCE> coals)
        {
            IEnumerable<double> weights = coals
                .Select(x => x.Weight)
                .OfType<double>()
                .Where(x => x != 0)
                .Distinct();

            foreach (var item in weights)
            {
                _coalsApiService.AddWeight(item);
            }
        }

        public void AddTypes(IEnumerable<CoalCE> coals)
        {
            IEnumerable<string> types = coals
                .Select(x => x.Type)
                .Where(x => x != null)
                .Distinct();

            foreach (var item in types)
            {
                _coalsApiService.AddType(item);
            }
        }
    }
}
