using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaCycle.Identity.Helpers
{
    public enum Enums
    {
        [Display(Description = "Administrator")]
        Administrator,

        [Display(Description = "Seller")]
        Seller
    }
}