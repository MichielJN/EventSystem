using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSystem.Models;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Extensions;

namespace EventSystem.Data
{
    public class EventDB
    {
        
        SQLiteAsyncConnection Database;
        SQLiteConnection Connection;

        public EventDB()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            
            await Database.CreateTableAsync<Happening>();
            await Database.CreateTableAsync<Invitee>();
            await Database.CreateTableAsync<HappeningMaker>();
        }
         public void Init2()
        {
            if (Connection is not null)
                return;

            Connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
            Connection.CreateTable<Happening>();
            Connection.CreateTable<Invitee>();
            Connection.CreateTable<HappeningMaker>();
        }
        //CRUD happening
        public async Task SaveHappening(Happening happening)
        {
            await Init();
            if(happening.Id == 0)
            {
                await Database.InsertAsync(happening);
                await Database.CloseAsync();
            }
            else
            {
                
            }
        }

        public async Task<List<Happening>> AlternativeGetHappenings()
        {
            Init2();
            return Connection.GetAllWithChildren<Happening>();
        }

        //CRUD HappeningMaker

        public async Task SaveHappeningMaker(HappeningMaker happeningMaker)
        {
            await Init();
            if(happeningMaker.Id == 0)
            {
                await Database.InsertAsync(happeningMaker);
                await Database.CloseAsync();
            }
            else
            {

            }
        }
        public async Task<HappeningMaker> GetHappeningMakerByName(String name)
        {
            await Init();

            List<HappeningMaker> happeningMakers = await Database.Table<HappeningMaker>().ToListAsync();
            
            HappeningMaker happeningMaker = happeningMakers.FirstOrDefault(m => m.Name == name);
            if (happeningMaker == null)
            {
                await Database.CloseAsync();
                return happeningMaker;
            }
            else
            {
                happeningMaker.Happenings = await GetHappeningsByHappeningMaker(happeningMaker);
                await Database.CloseAsync();
                return happeningMaker;
            }
            
        }

        public async Task<HappeningMaker> GetHappeningMakerByEmail(String email)
        {
            await Init();

            List<HappeningMaker> happeningMakers = await Database.Table<HappeningMaker>().ToListAsync();

            HappeningMaker happeningMaker = happeningMakers.FirstOrDefault(m => m.Email == email);
            if (happeningMaker == null)
            {
                await Database.CloseAsync();
                return happeningMaker;
            }
            else
            {
                happeningMaker.Happenings = await GetHappeningsByHappeningMaker(happeningMaker);
                await Database.CloseAsync();
                return happeningMaker;
            }

        }

        public async Task<List<HappeningMaker>> GetAllHappeningMakers()
        {
            await Init();
            List<HappeningMaker> happeningMakers = await Database.Table<HappeningMaker>().ToListAsync();
            foreach(HappeningMaker happeningMaker in happeningMakers)
            {
                happeningMaker.Happenings = await GetHappeningsByHappeningMaker(happeningMaker);
                foreach(Happening happening in happeningMaker.Happenings)
                {
                    happening.Invitees = await GetInviteesOfHappening(happening);
                }
            }
            await Database.CloseAsync();
            return happeningMakers;
        }


        //Crud Happening

        public async Task<List<Happening>> GetHappeningsByHappeningMaker(HappeningMaker happeningMaker)
        {
            await Init();
            List<Happening> happenings = await Database.Table<Happening>().ToListAsync();
            await Database.CloseAsync();
            
            try
            {
                happenings = happenings.FindAll(x => x.HappeningMakerId == happeningMaker.Id);
                foreach(Happening happening in happenings)
                {
                    happening.HappeningMaker = happeningMaker;
                }
                return happenings;
            }
            catch
            {
                return new List<Happening>();
            }
        }


        public async Task<Happening> GetHappeningByName(string happeningName)
        {
            await Init();
            List<Happening> happenings = await Database.Table<Happening>().ToListAsync();
            List<HappeningMaker> happeningMakers = await Database.Table<HappeningMaker>().ToListAsync();
            Happening happening = happenings.FirstOrDefault(x => x.Name == happeningName);
            happening.Invitees = await GetInviteesOfHappening(happening);
            happening.HappeningMaker = await GetHappeningMakerByName(happeningMakers.FirstOrDefault(x => x.Id == happening.HappeningMakerId).Name);
            //string name = happening.HappeningMaker.Name;
            //appeningMaker happeningMaker = await GetHappeningMakerByName(name);
           // happening.HappeningMaker = happeningMaker;
            Database.CloseAsync();
            return happening;
        }

        public async Task<List<Happening>> GetAllHappenings()
        {
            await Init();
            List<Happening> happenings = await Database.Table<Happening>().ToListAsync();
            List<HappeningMaker> happeningMakers = await GetAllHappeningMakers();
            foreach(Happening happening in happenings)
            {
                happening.HappeningMaker = happeningMakers.FirstOrDefault(x => x.Id == happening.HappeningMakerId);
                happening.Invitees = await GetInviteesOfHappening(happening);
            }
            Database.CloseAsync();
            return happenings;
        }

        //CRUD Invitee
        
        public async Task SaveOrUpdateInvitee(Invitee invitee)
        {
            await Init();
            if(invitee.Id == 0)
            {
                await Database.InsertAsync(invitee);
                await Database.CloseAsync();
            }
            else
            {

            }
        }
        public async Task<List<Invitee>> GetInviteesOfHappening(Happening happening)
        {
            await Init();
            List<Invitee> invitees = await Database.Table<Invitee>().ToListAsync();
            await Database.CloseAsync();

            try
            {
                invitees = invitees.FindAll(x => x.HappeningId == happening.Id);
                foreach(Invitee invitee in invitees)
                {
                    invitee.Happening = happening;
                }
                return invitees; 
            
            }
            catch
            {
                return new List<Invitee>();
            }
        }

        
    }
}
