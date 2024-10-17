﻿using Form_Page.Models;
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
                products = products.Where(p=>p.CategoryId ==int.Parse(category)).ToList();

//                String.IsNullOrEmpty(category): Bu ifade, category değişkeninin boş(empty) veya null olup olmadığını kontrol eder. Eğer category boş değilse, kod çalışmaya devam eder.
//                products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();:

//                products: Ürünlerin yer aldığı bir koleksiyondur.Bu ürünler muhtemelen bir veritabanından alınmıştır ve CategoryId alanına sahiptir(her ürünün bir kategori kimliği var).
//                products.Where(...): LINQ sorgusu kullanılarak ürünler filtrelenir.Sorgu, CategoryId'si, kullanıcının seçtiği category değerine eşit olan ürünleri bulur.
//                p.CategoryId == int.Parse(category): Bu ifade, her bir ürünün(p) CategoryId özelliği ile seçilen kategori değerini karşılaştırır.Kategori değeri bir string olarak geldiğinden, int.Parse(category) ifadesiyle sayıya dönüştürülür ve CategoryId ile kıyaslanır.

            }

            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
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
