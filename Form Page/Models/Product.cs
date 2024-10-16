using System.ComponentModel.DataAnnotations;

namespace Form_Page.Models
{
    public class Product
    {
        [Display(Name="Urun Id")]
        public int ProductId { get; set; }
        [Display(Name = "Urun Adı")]

        public string? Name { get; set; }
        [Display(Name = "Urun Fiyatı")]

        public decimal Price { get; set; }
        [Display(Name = "Urun Gorseli")]

        public string? Image { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
    }
}
