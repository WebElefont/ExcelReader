using ExcelReader.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReader.Services
{
    public class AddLiquidsService
    {
        private readonly ConfigureImagesService _addImagesService;
        private readonly ConfigureProducersService _addProducersService;

        private readonly LiquidsApiService _liquidsApiService;

        public AddLiquidsService(ConfigureImagesService addImagesService, ConfigureProducersService addProducersService, LiquidsApiService liquidsApiService)
        {
            _liquidsApiService = liquidsApiService;
            _addImagesService = addImagesService;
            _addProducersService = addProducersService;
        }

        public void SendToApi(IEnumerable<LiquidCE> liquids)
        {
            AddNicotineTypes(liquids);
            AddNicotineStrengths(liquids);
            AddCapacities(liquids);
            AddTastes(liquids);

            _addProducersService.ConfigureProducers(liquids);
            _addImagesService.ConfigureImages(liquids);

            foreach (var liquid in liquids)
            {
                _liquidsApiService.AddGood(liquid);
            }
        }

        private void AddNicotineTypes(IEnumerable<LiquidCE> liquids)
        {
            IEnumerable<string> apiNicotineTypes = _liquidsApiService.GetNicotineType();
            IEnumerable<string> liquidsNicotineTypes = liquids
                .Select(x => x.NicotineType)
                .Distinct();

            foreach (string item in liquidsNicotineTypes)
                if (apiNicotineTypes.Contains(item) == false)
                    _liquidsApiService.AddNicotineType(item);
        }

        private void AddNicotineStrengths(IEnumerable<LiquidCE> liquids)
        {
            IEnumerable<byte> apiNicotineStrength = _liquidsApiService.GetNicotineStrength();
            IEnumerable<byte> liquidsNicotineStrength = liquids
                .Select(x => x.NicotineStrength)
                .Distinct();

            foreach (byte item in liquidsNicotineStrength)
                if (apiNicotineStrength.Contains(item) == false)
                    _liquidsApiService.AddNicotineStrength(item);
        }

        private void AddCapacities(IEnumerable<LiquidCE> liquids)
        {
            IEnumerable<byte> apiCapacities = _liquidsApiService.GetCapacity();
            IEnumerable<byte> liquidsCapacities = liquids
                .Select(x => x.Capacity)
                .Distinct();

            foreach (byte item in liquidsCapacities)
                if (apiCapacities.Contains(item) == false)
                    _liquidsApiService.AddCapacity(item);
        }

        private void AddTastes(IEnumerable<LiquidCE> liquids)
        {
            var liquidTastes = liquids
                .Select(x => new Taste(x))
                .Distinct();

            var liquidGroups = liquids
                .Select(x => x.TasteGroup)
                .Distinct();

            foreach (var group in liquidGroups)
            {
                _liquidsApiService.AddTasteGroup(group);
                foreach (var item in liquidTastes.Where(x => x.Group == group))
                {
                    _liquidsApiService.AddTaste(item.Group, item.Name);
                }
            }
        }

        private class Taste : IEquatable<Taste>
        {
            public Taste(string name, string group)
            {
                Name = name;
                Group = group;
            }

            public Taste(LiquidCE liquid)
            {
                Name = liquid.Taste;
                Group = liquid.TasteGroup;
            }

            public string Name { get; set; }
            public string Group { get; set; }

            public bool Equals(Taste? other)
            {
                if (Object.ReferenceEquals(other, null)) return false;
                if (Object.ReferenceEquals(this, other)) return true;

                return Group.Equals(other.Group) && Name.Equals(other.Name);
            }

            public override int GetHashCode()
            {
                int hashTasteName = Name == null ? 0 : Name.GetHashCode();
                int hashTasteGroup = Group == null ? 0 : Group.GetHashCode();

                return hashTasteName ^ hashTasteGroup;
            }
        }
    }
}
