using EventSystem.Models;

namespace EventSystem.Pages;

public partial class ManageInvitee_Page : ContentPage
{
	private Invitee invitee;
	public ManageInvitee_Page(Invitee _invitee)
	{
		
		InitializeComponent();
        this.invitee = _invitee;
		if(invitee.IsInHappening == true)
		{
			IsInHappening_Switch.IsToggled = true;
		}
		this.Name_Label.Text = "Naam: " + invitee.Name;
		this.Email_Label.Text = "Email: " + invitee.Email;
    }
    public void IsInHappening_Toggled(object sender, EventArgs e)
	{
		if(IsInHappening_Switch.IsToggled == false)
		{
			this.invitee.IsInHappening = false;
			App.InviteeRepo.SaveEntity(invitee);

        }
		else
		{
            this.invitee.IsInHappening = true;
            App.InviteeRepo.SaveEntity(invitee);
        }
		
	}

	public async void BackButton_Pressed(object sender, EventArgs e)
	{
		Application.Current.MainPage.Navigation.PushModalAsync(new EventHome_Page(App.HappeningRepo.GetEntityWithChildren(invitee.HappeningId)));
	}
}