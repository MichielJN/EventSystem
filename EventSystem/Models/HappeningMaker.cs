using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSystem.Abstractions;

namespace EventSystem.Models
{
    public class HappeningMaker : TableData
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Happening> Happenings { get; set; } = new List<Happening>();
        public HappeningMaker() 
        {
            
        }

        public HappeningMaker(string name, string password, string email)//for inserting in database
        {
            Id = 0;
            Name = name;
            this.Password = password;
            this.Email = email;

        }
        public HappeningMaker(string email, string password)//for logging in
        {
            this.Email = email;
            this.Password = password;
        }

    }
}
