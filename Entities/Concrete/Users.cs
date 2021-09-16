using Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Users : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Permisson { get; set; }

    }
}
