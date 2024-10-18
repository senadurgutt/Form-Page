using Form_Page.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Index(string searchString, string category) //searchString  ürünler içinde arama yapmak için kullanılır
        {
            var products = Repository.Products;  //products, ürünleri saklayan bir değişkendir ve Repository.Products üzerinden alınır.
            
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.SearchString = searchString; //arama yapılan kelimenin arama butonunda kalması için kullanılıyor
                products = products.Where(p=> p.Name.Contains(searchString)).ToList();

//                ürün listesini filtreler.products içindeki her bir ürünün Name(isim) özelliği, kullanıcının girdiği searchString ifadesini içeriyorsa, o ürün listede kalır.
//                Contains(searchString): Arama terimi, ürün isminin içinde geçiyor mu diye kontrol eder.
//                ToList(): Sonucu bir listeye dönüştürür.
            }

            if (!String.IsNullOrEmpty(category) && category != "0") 
            {
                products = products.Where(p=>p.CategoryId == int.Parse(category)).ToList();

//                String.IsNullOrEmpty(category): Bu ifade, category değişkeninin boş(empty) veya null olup olmadığını kontrol eder. Eğer category boş değilse, kod çalışmaya devam eder.
//                products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();:
//                products: Ürünlerin yer aldığı bir koleksiyondur.Bu ürünler muhtemelen bir veritabanından alınmıştır ve CategoryId alanına sahiptir(her ürünün bir kategori kimliği var).
//                products.Where(...): LINQ sorgusu kullanılarak ürünler filtrelenir.Sorgu, CategoryId'si, kullanıcının seçtiği category değerine eşit olan ürünleri bulur.
//                p.CategoryId == int.Parse(category): Bu ifade, her bir ürünün(p) CategoryId özelliği ile seçilen kategori değerini karşılaştırır.Kategori değeri bir string olarak geldiğinden, int.Parse(category) ifadesiyle sayıya dönüştürülür ve CategoryId ile kıyaslanır.

            }

            //ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name",category);
            var model = new ProductViewModel
            {
                Products = products,
                Categories = Repository.Categories,
                SelectedCategory = category
            };
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product model)
        {
            Repository.CreateProduct(model);
            return RedirectToAction("Index"); // sayfanın view i çalışmıyor da index sayfasını döndürüyor.
        }
    }
}
