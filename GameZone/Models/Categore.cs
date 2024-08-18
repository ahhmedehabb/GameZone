

namespace GameZone.Models
{
    public class Categore :BaseEntity
    {
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
