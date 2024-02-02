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
    public class Invitee : TableData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public bool IsInHappening { get; set; } = false;
        [ForeignKey(typeof(Happening))]
        public int HappeningId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Happening Happening { get; set; }

        public Invitee()
        {

        }
        public Invitee(string name, string email, int happeningId)//for inserting in database
        {
            this.Id = 0;
            this.Name = name;
            this.Email = email;
            this.HappeningId = happeningId;
        }

        public string GetQRStringInvitee()
        {
            return this.Email + ";" + this.Happening.Name;
        }
    }
}
