using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasty.Backend.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Restaurant
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public double Latitude { get; set; }
        [Required] public double Longitude { get; set; }
        [Required] public double Radius { get; set; }
        public IList<Cuisine> Cuisines { get; set; } = new List<Cuisine> { };
    }
}
