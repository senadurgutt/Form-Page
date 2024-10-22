using System.ComponentModel.DataAnnotations;

namespace Form_Page.Models
{
    public class Product
    {
        [Display(Name="Urun Id")]
        [Required(ErrorMessage ="Hatalı isim")]
        public int ProductId { get; set; }
        [Display(Name = "Urun Adı")]

        [Required]
        [StringLength(50)] // 100 karakter sınırlaması 
        public string? Name { get; set; }
        [Range(0,1000000)] //fiyat için kısıtlama verdi
        [Display(Name = "Urun Fiyatı")]

        public decimal Price { get; set; }
        [Display(Name = "Urun Gorseli")]

        public string? Image { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "Category")]

        public int CategoryId { get; set; }
    }
}
