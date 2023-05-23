using ExcelReader.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReader.Services
{
    public class ConfigureImagesService
    {
        private readonly ImagesApiService _imagesApiService;

        public ConfigureImagesService(ImagesApiService imagesApiService)
        {
            _imagesApiService = imagesApiService;
        }

        public void ConfigureImages<T>(IEnumerable<T> goods) where T : GoodCE
        {
            foreach (GoodCE liquid in goods)
            {
               
                    liquid.ImageId = _imagesApiService.AddImage(liquid.ImageUrl);
                    //liquid.ImageId = new Guid("00000000-0000-0000-0000-000000000001");
            }
        }
    }
}
