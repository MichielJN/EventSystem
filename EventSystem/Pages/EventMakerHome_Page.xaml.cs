using EventSystem.Data;
using EventSystem.Models;
using EventSystem.Pages;

namespace EventSystem;


public partial class EventMakerHome_Page : ContentPage
{
	private EventDB db = new EventDB();
	private HappeningMaker happeningMaker;

	public EventMakerHome_Page(HappeningMaker _happeningMaker)
	{
		InitializeComponent();
        this.happeningMaker = _happeningMaker;
		try
		{
			happeningMaker.Happenings = App.HappeningRepo.GetEntitiesWithChildren().FindAll(x => x.HappeningMakerId == happeningMaker.Id);

        }
		catch
		{

		}
		


		this.Happening_Collection.ItemsSource = happeningMaker.Happenings.ToList();
	}
	public async void BackButton_Pressed(object sender, EventArgs e)
	{
		Application.Current.MainPage.Navigation.PushModalAsync(new MainPage());
	}

	public async void MakeHappeningButton_Pressed(object sender, EventArgs e)
	{
		Application.Current.MainPage.Navigation.PushModalAsync(new CreateEvent_Page(App.HappeningMakerRepo.GetEntityWithChildren(this.happeningMaker.Id)));
	}

	public async void SelectHappeningButtonPressed(object sender, EventArgs e)
	{
		var name = ((Button)sender).Text;
		await Application.Current.MainPage.Navigation.PushModalAsync(new EventHome_Page(await db.GetHappeningByName(((Button)sender).Text)));
	}
}