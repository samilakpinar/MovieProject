using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string SessionId { get; set; }
        public int Permisson { get; set; }

        public User(int Id, string Name, string Surname, string Email, string Password, string Token, string SessionId, int Permisson)
        {
            this.Id = Id;
            this.Name = Name;
            this.Surname = Surname;
            this.Email = Email;
            this.Password = Password;
            this.Token = Token;
            this.SessionId = SessionId;
            this.Permisson = Permisson;
        }

    }
}
