using Microsoft.AspNetCore.Identity;

namespace PruebaTecnicaCycle.Identity.Models
{
    public class ApplicationUser : IdentityUser// se usa para tener acceso a propiedades, estas son las que se agregaran a la BD
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}