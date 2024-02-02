using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensions.Attributes;
using EventSystem.Abstractions;


namespace EventSystem.Models
{
    public class Happening : TableData
    {
        public string Name { get; set; }
        [OneToMany(CascadeOperations =CascadeOperation.All)]
        public List<Invitee>? Invitees { get; set; } = new List<Invitee>();

        [ForeignKey(typeof(HappeningMaker))]
        public int HappeningMakerId { get; set; }

        [ManyToOne(CascadeOperations =CascadeOperation.All)]
        public HappeningMaker HappeningMaker { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public int MaxAmountIvitees { get; set; }
        public Happening()
        {

        }

        public Happening(string name, int happeningMakerId, DateTime date, DateTime startTime, DateTime endTime)//for inserting in database  //, DateTime startTime, DateTime endTime
        {
            this.Name = name;
            this.HappeningMakerId = happeningMakerId;
            this.Id = 0;
            this.Date = date;
            this.StartTime = startTime;
            this.EndTime = endTime;
            
        }

        public List<Invitee> GetInviteesNotInHappening()
        {
            return this.Invitees.FindAll(x => x.IsInHappening == false);
        }

        public List<Invitee> GetInviteesInHappening()
        {
            return this.Invitees.FindAll(x => x.IsInHappening == true);
        }
    }
    
}
