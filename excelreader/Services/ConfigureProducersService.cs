using ExcelReader.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReader.Services
{
    public class ConfigureProducersService
    {
        private readonly ProducersApiService _producersApiService;

        public ConfigureProducersService(ProducersApiService producersApiService)
        {
            _producersApiService = producersApiService;
        }

        public void ConfigureProducers<T>(IEnumerable<T> goods) where T : GoodCE
        {
            IEnumerable<ProducerCE> producers = _producersApiService.GetProducers();

            foreach (GoodCE good in goods)
            {
                ProducerCE producer = producers.First(x => x.Name.ToLower() == good.ProducerName.ToLower()); // try catch етсь ли продюсер
                good.ProducerId = producer.Id;
            }
        }
    }
}
