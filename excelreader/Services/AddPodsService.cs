using ExcelReader.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReader.Services
{
    public class AddPodsService
    {
        private readonly ConfigureImagesService _addImagesService;
        private readonly ConfigureProducersService _addProducersService;

        private readonly PodsApiService _podsApiService;

        public AddPodsService(ConfigureImagesService addImagesService, ConfigureProducersService addProducersService, PodsApiService podsApiService)
        {
            _podsApiService = podsApiService;
            _addImagesService = addImagesService;
            _addProducersService = addProducersService;
        }

        public void SendToApi(IEnumerable<PodCE> pods)
        {
            AddWeights(pods);
            AddMaterials(pods);
            AddEvaporatorResistances(pods);
            AddPowers(pods);
            AddBattareys(pods);
            AddCartrigeCapacities(pods);   
            AddPorts(pods);

            _addImagesService.ConfigureImages(pods);
            _addProducersService.ConfigureProducers(pods);

            foreach(var pod in pods)
            {
                _podsApiService.AddGood(pod);
            }
        }

        public void AddWeights(IEnumerable<PodCE> pods)
        {
            IEnumerable<double> weights = pods
                .Select(x => x.Weight)
                .OfType<double>()
                .Where(x => x != 0)
                .Distinct();

            foreach (var item in weights) 
            {
                _podsApiService.AddWeight(item);
            }
        }

        public void AddMaterials(IEnumerable<PodCE> pods)
        {
            IEnumerable<string> materials = pods
                .Select(x => x.Material)
                .Where(x => x != null)
                .Distinct();

            foreach(var item in materials)
            {
                _podsApiService.AddMaterial(item);
            }
        }

        public void AddEvaporatorResistances(IEnumerable<PodCE> pods)
        {
            IEnumerable<double> resistances = pods
                .Select(x => x.EvaporatorResistance)
                .OfType<double>()
                .Where (x => x != 0)
                .Distinct();

            foreach(var item in resistances)
            {
                _podsApiService.AddEvaporatorResistance(item);
            }
        }

        public void AddPowers(IEnumerable<PodCE> pods)
        {
            IEnumerable<string> powers = pods
                .Select(x => x.Power)
                .Where (x => x != null)
                .Distinct();

            foreach (var item in powers)
            {
                _podsApiService.AddPower(item);
            }
        }

        public void AddBattareys(IEnumerable<PodCE> pods)
        {
            IEnumerable<short> battareys = pods
                .Select(x => x.Battarey)
                .Where (x => x != 0)
                .Distinct();

            foreach( var item in battareys)
            {
                _podsApiService.AddBattarey(item);
            }
        }

        public void AddCartrigeCapacities(IEnumerable<PodCE> pods)
        {
            IEnumerable<double> capacities = pods
                .Select(x => x.CartrigeCapacity)
                .OfType<double>()
                .Where(x => x != 0)
                .Distinct();

            foreach(var item in capacities)
            {
                _podsApiService.AddCartrigeCapacity(item);
            }
        }

        public void AddPorts(IEnumerable<PodCE> pods)
        {
            IEnumerable<string> ports = pods
                .Select(x => x.Port)
                .Where(x => x != null)
                .Distinct();

            foreach (var item in ports)
            {
                _podsApiService.AddPort(item);
            }
        }
    }
}
