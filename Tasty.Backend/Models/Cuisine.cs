using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Tasty.Backend.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Cuisine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public string Name { get; set; }

        [JsonIgnore]
        public IList<Restaurant> Restaurants { get; set; } = new List<Restaurant> { };
    }
}
