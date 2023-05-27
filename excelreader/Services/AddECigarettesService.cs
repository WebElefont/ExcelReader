using ExcelReader.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReader.Services
{
    public class AddECigarettesService
    {
        private readonly ConfigureImagesService _addImagesService;
        private readonly ConfigureProducersService _addProducersService;

        private readonly ECigarettesApiService _eCigarettesApiService;

        public AddECigarettesService(ConfigureImagesService addImagesService, ConfigureProducersService addProducersService, ECigarettesApiService addECigarettesService)
        {
            _eCigarettesApiService = addECigarettesService;
            _addImagesService = addImagesService;
            _addProducersService = addProducersService;
        }

        public void SendToApi(IEnumerable<ECigaretteCE> eCigarettes)
        {
            AddBattareyCapacities(eCigarettes);
            AddTastes(eCigarettes);
            AddEvaporatorVolumes(eCigarettes);
            AddPuffsCount(eCigarettes);

            _addImagesService.ConfigureImages(eCigarettes);
            _addProducersService.ConfigureProducers(eCigarettes);

            foreach(var eCigarette in eCigarettes)
            {
                _eCigarettesApiService.AddGood(eCigarette);
            }
        }

        public void AddBattareyCapacities(IEnumerable<ECigaretteCE> eCigarettes)
        {
            IEnumerable<short> battareys = eCigarettes
                .Select(x => x.BattareyCapacity)
                .Where(x => x != 0)
                .Distinct();

            foreach (var item in battareys)
            {
                _eCigarettesApiService.AddBattareyCapacity(item);
            }
        }

        public void AddTastes(IEnumerable<ECigaretteCE> eCigarettes)
        {
            IEnumerable<string> tastes = eCigarettes
                .Select(x => x.Taste)
                .Where(x => x != null)
                .Distinct();

            foreach (var item in tastes)
            {
                _eCigarettesApiService.AddTaste(item);
            }
        }

        public void AddEvaporatorVolumes(IEnumerable<ECigaretteCE> eCigarettes)
        {
            IEnumerable<byte> volumes = eCigarettes
                .Select(x => x.EvaporatorVolume)
                .Where(x => x != 0)
                .Distinct();

            foreach (var item in volumes)
            {
                _eCigarettesApiService.AddEvaporatorVolume(item);
            }
        }

        public void AddPuffsCount(IEnumerable<ECigaretteCE> eCigarettes)
        {
            IEnumerable<int> counts = eCigarettes
                .Select(x => x.PuffsCount)
                .Where(x => x != 0)
                .Distinct();

            foreach (var item in counts)
            {
                _eCigarettesApiService.AddPuffsCount(item);
            }
        }
    }
}
