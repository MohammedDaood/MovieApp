using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Data.Model.Genre
{
    public class Genre
    {
        public byte Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
