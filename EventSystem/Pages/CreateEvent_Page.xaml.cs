using EventSystem.Models;
using EventSystem.Data;

namespace EventSystem.Pages;

public partial class CreateEvent_Page : ContentPage
{
	private EventDB db = new EventDB();
	private HappeningMaker happeningMaker;
	public CreateEvent_Page(HappeningMaker _happeningMaker)
	{
		InitializeComponent();
		this.happeningMaker = _happeningMaker;
	}

	public async void BackButton_Pressed(object sender, EventArgs e)
	{
		await Application.Current.MainPage.Navigation.PushModalAsync(new EventMakerHome_Page(App.HappeningMakerRepo.GetEntityWithChildren(happeningMaker.Id)));
	}

	public async void MakeHappeningButton_Pressed(object sender, EventArgs e)
	{
		

		if (App.HappeningRepo.GetEntitiesWithChildren().FirstOrDefault(x => x.Name == HappeningName_Entry.Text) == null)
		{

			if (HappeningName_Entry.Text != "")
			{
				//await db.SaveHappening(new Happening(HappeningName_Entry.Text, this.happeningMaker.Id));
				
				App.HappeningRepo.SaveEntity(new Happening(HappeningName_Entry.Text, this.happeningMaker.Id, DateTime.Parse(Happening_DatePicker.Date.ToString()), DateTime.Parse(Begin_TimePicker.Time.ToString()), DateTime.Parse(End_TimePicker.Time.ToString())));
				await Application.Current.MainPage.Navigation.PushModalAsync(new EventMakerHome_Page(App.HappeningMakerRepo.GetEntitiesWithChildren().FirstOrDefault(x => x.Name == happeningMaker.Name)));
			}
			else
			{
				HappeningName_Entry.Text = "Voer een naam in";
			}
		}
		else
		{
			HappeningName_Entry.Text = "";
			HappeningName_Entry.Placeholder = "Er bestaat al een evenement met deze naam.";
		}
	}
}