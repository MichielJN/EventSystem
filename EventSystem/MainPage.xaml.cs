﻿using EventSystem.Pages;
using EventSystem.Data;
using EventSystem.Models;

namespace EventSystem
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        EventDB db = new EventDB();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void CreateHappeningMaker_Pressed(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new CreateHappeningMaker_Page());
        }

        public async void Showlist_pressed(object sender, EventArgs e)
        {
            List<HappeningMaker> happeningMakers  = App.HappeningMakerRepo.GetEntitiesWithChildren();
            HappeningMaker happeningMaker = happeningMakers[0];
            
        }

        private async void LogEventMakerIn_Pressed(object sender, EventArgs e)
        {
            HappeningMaker happeningMaker = new HappeningMaker(this.HappeningMakerEmail_Entry.Text, this.HappeningMakerPassword_Entry.Text);
            List<HappeningMaker> happeningMakers = App.HappeningMakerRepo.GetEntitiesWithChildren();

                if (App.HappeningMakerRepo.GetEntitiesWithChildren().FirstOrDefault(x => x.Email == HappeningMakerEmail_Entry.Text) != null)
                {
                    if (happeningMaker.Password == App.HappeningMakerRepo.GetEntitiesWithChildren().FirstOrDefault(x => x.Email == HappeningMakerEmail_Entry.Text).Password)
                    {
                        happeningMaker = App.HappeningMakerRepo.GetEntitiesWithChildren().FirstOrDefault(x => x.Email == HappeningMakerEmail_Entry.Text);
                        // happeningMaker.Happenings = App.HappeningRepo.GetEntitiesWithChildren().FindAll(x => x.HappeningMakerId == happeningMaker.Id);
                        Application.Current.MainPage.Navigation.PushModalAsync(new EventMakerHome_Page(happeningMaker));
                    }
                    else
                    {
                        HappeningMakerEmail_Entry.Text = "";
                        HappeningMakerEmail_Entry.Placeholder = "Verkeerde gegevens";
                        HappeningMakerPassword_Entry.Text = "";
                        HappeningMakerPassword_Entry.Placeholder = "Verkeerde inloggegevens";
                    }
                }
                else
                {
                    HappeningMakerEmail_Entry.Text = "";
                    HappeningMakerEmail_Entry.Placeholder = "Verkeerde gegevens";
                    HappeningMakerPassword_Entry.Text = "";
                    HappeningMakerPassword_Entry.Placeholder = "Verkeerde inloggegevens";


                }
            }
        

    }

}