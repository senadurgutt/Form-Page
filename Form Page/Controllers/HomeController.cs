using Form_Page.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.IO;

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
                products = products.Where(p => p.Name!.Contains(searchString)).ToList();

                //                ürün listesini filtreler.products içindeki her bir ürünün Name(isim) özelliği, kullanıcının girdiği searchString ifadesini içeriyorsa, o ürün listede kalır.
                //                Contains(searchString): Arama terimi, ürün isminin içinde geçiyor mu diye kontrol eder.
                //                ToList(): Sonucu bir listeye dönüştürür.
            }

            if (!String.IsNullOrEmpty(category) && category != "0")
            {
                products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();

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
        public async Task<IActionResult> Create(Product model, IFormFile imageFile) //productlar model içerisine kayıt edildiği için form file için başka bir şey açman gerekiyordu onu da böyle kaydetmek için bu şekilde yazdık
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png"};
            var extension = Path.GetExtension(imageFile.FileName);
            var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");  // extension yerine .jpg de yazabilirdin 
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName); //Directory.GetCurrentDirectory() kalıp olarak bulunduğum dizine gelir root olarak da görseller wwwroot/img altında olduğu için arkasından da kaydedilmesi için dosya ismini alıyoruz

            if (imageFile != null)
            {
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("", "Geçerli resim seçiniz");
                }
            }
            if (ModelState.IsValid)  /*isvalid özelliği product modeldeki her şeyin kuralına uygun gelip gelmediğini kontrol eder*/
            {
                if(imageFile != null) 
                {
                    using (var stream = new FileStream(path, FileMode.Create)) //using ifadesi doşya akışı açılır ve kullanıldıktan sonra kapanır
                    { //filestream dosyaya veri yazmak ve okumak için kullanılr
                        await imageFile.CopyToAsync(stream);
                    }
                
                    model.Image = randomFileName;
                }
                model.ProductId = Repository.Products.Count + 1;
                    Repository.CreateProduct(model);
                    return RedirectToAction("Index"); // sayfanın view i çalışmıyor da index sayfasını döndürüyor.
        }
                ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
                return View(model); //eğer her şey yolunda değilse aynı sayfa yazdıklarıyla birlikte tekrar ona dönecek
            
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
            if (entity == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
            return View(entity);
        }
    
    [HttpPost]
        public async Task<IActionResult> Edit(int id, Product model, IFormFile? imageFile)
        {
            if(id != model.ProductId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

                if (imageFile != null)
                {
                    var extension = Path.GetExtension(imageFile.FileName);
                    var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");  // extension yerine .jpg de yazabilirdin 
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName); //Directory.GetCurrentDirectory() kalıp olarak bulunduğum dizine gelir root olarak da görseller wwwroot/img altında olduğu için arkasından da kaydedilmesi için dosya ismini alıyoruz

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    model.Image = randomFileName;
                }
                Repository.UpdateProduct(model);

                return RedirectToAction("Index"); // Başarılı edit işleminden sonra index sayfasına yönlendir
            }
            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
    return View(model);
        
        }
    }  
}
