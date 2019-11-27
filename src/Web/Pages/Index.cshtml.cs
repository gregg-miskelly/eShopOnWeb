using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi;

namespace Microsoft.eShopWeb.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogViewModelService _catalogViewModelService;
        private readonly IConfiguration _configuration;
        private readonly IRequestProvider _requestProvider;

        public IndexModel(ICatalogViewModelService catalogViewModelService, IConfiguration configuration, IRequestProvider requestProvider)
        {
            _catalogViewModelService = catalogViewModelService;
            _configuration = configuration;
            _requestProvider = requestProvider;
        }

        public CatalogIndexViewModel CatalogModel { get; set; } = new CatalogIndexViewModel();

        public async Task OnGet(CatalogIndexViewModel catalogModel, int? pageId)
        {
            CatalogModel = await _catalogViewModelService.GetCatalogItems(pageId ?? 0, Constants.ITEMS_PER_PAGE, catalogModel.BrandFilterApplied, catalogModel.TypesFilterApplied);

            string webApiUrl = _configuration.GetConnectionString("WebApi");
            var forcast = await _requestProvider.GetAsync<List<WeatherForecast>>(webApiUrl);
            if (forcast.Count > 0)
            {
                CatalogModel.TommorowTemp = forcast[0].TemperatureF.ToString();
            }
        }


    }
}
