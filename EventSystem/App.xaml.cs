using EventSystem.Data;
using EventSystem.Models;

namespace EventSystem
{
    public partial class App : Application
    {
        public static BaseRepository<HappeningMaker>? HappeningMakerRepo { get; private set; }
        public static BaseRepository<Happening>? HappeningRepo {  get; private set; }
        public static BaseRepository<Invitee>? InviteeRepo { get; private set; }
        public App(BaseRepository<HappeningMaker>? happeningMakerRepo, BaseRepository<Happening>? happeningRepo, BaseRepository<Invitee>? inviteeRepo)
        {
            InitializeComponent();

            HappeningMakerRepo = happeningMakerRepo;
            InviteeRepo = inviteeRepo;
            HappeningRepo = happeningRepo;

            MainPage = new AppShell();
        }
    }
}
