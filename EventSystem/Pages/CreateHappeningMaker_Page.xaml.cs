using EventSystem.Data;
using EventSystem.Models;

namespace EventSystem.Pages;

public partial class CreateHappeningMaker_Page : ContentPage
{
	private EventDB db = new EventDB();
	public CreateHappeningMaker_Page()
	{
		InitializeComponent();
	}
	public async void CreateHappeningMaker_Pressed(object sender, EventArgs e)
	{
		if (HappeningMakerName_Entry.Text != "" && HappeningMakerPassword_Entry.Text != "")
		{
			if (HappeningMakerPassword_Entry.Text == ConfirmHappeningMakerPassword_Entry.Text)
			{


					if (App.HappeningMakerRepo.GetEntitiesWithChildren().FirstOrDefault(x => x.Email == Email_entry.Text) == null)
					{
						App.HappeningMakerRepo.SaveEntity(new HappeningMaker(HappeningMakerName_Entry.Text, HappeningMakerPassword_Entry.Text, Email_entry.Text));
					    await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage());

					}
					else
					{
						Email_entry.Text = "";
						Email_entry.Placeholder = "E-mail is al in gebruik";
					}

				
            }
			else
			{
				HappeningMakerPassword_Entry.Text = "";
				ConfirmHappeningMakerPassword_Entry.Text = "";
                HappeningMakerPassword_Entry.Placeholder = "Wachtwoorden komen niet overeen";
                ConfirmHappeningMakerPassword_Entry.Placeholder = "Wachtwoorden komen niet overeen";
            }
			
		}
	}

	public async void BackButton_Pressed(object sender, EventArgs e)
	{
        Application.Current.MainPage.Navigation.PushModalAsync(new MainPage());
    }
}