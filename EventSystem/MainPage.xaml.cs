using EventSystem.Pages;
using EventSystem.Data;
using EventSystem.Models;
using EventSystem.Logic;
using Newtonsoft;
using System.Text.Json;
using Newtonsoft.Json.Linq;


namespace EventSystem
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        EventDB db = new EventDB();

        public MainPage()
        {
            InitializeComponent();
            ToggleShake();
        }

        private async void CreateHappeningMaker_Pressed(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new CreateHappeningMaker_Page());
        }

        public async void Showlist_pressed(object sender, EventArgs e)
        {
            //ShowList.Text = await EventSystem.Logic.JokeLogic.GetRandomJoke();
            var joke = JObject.Parse(await EventSystem.Logic.JokeLogic.GetRandomJoke());
            var showJoke = (string)joke["attachments"][0]["text"];
            ShowList.Text = showJoke + " ...........Klik voor een andere grap";
           
            

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

        private void ToggleShake()
        {
            if (Accelerometer.Default.IsSupported)
            {
                if (!Accelerometer.Default.IsMonitoring)
                {
                    // Turn on accelerometer
                    Accelerometer.Default.ShakeDetected += Accelerometer_ShakeDetected;
                    Accelerometer.Default.Start(SensorSpeed.Game);
                }
                else
                {
                    // Turn off accelerometer
                    Accelerometer.Default.Stop();
                    Accelerometer.Default.ShakeDetected -= Accelerometer_ShakeDetected;
                }
            }
        }
        private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            // Update UI Label with a "shaked detected" notice, in a randomized color
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
