using Form_Page.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Form_Page.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string searchString) //searchString  ürünler içinde arama yapmak için kullanılır
        {
            var products = Repository.Products;  //products, ürünleri saklayan bir değişkendir ve Repository.Products üzerinden alınır.
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p=> p.Name.Contains(searchString)).ToList();

//                ürün listesini filtreler.products içindeki her bir ürünün Name(isim) özelliği, kullanıcının girdiği searchString ifadesini içeriyorsa, o ürün listede kalır.
//                Contains(searchString): Arama terimi, ürün isminin içinde geçiyor mu diye kontrol eder.
//                ToList(): Sonucu bir listeye dönüştürür.

            }
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
