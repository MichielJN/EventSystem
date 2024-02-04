using EventSystem.Models;

namespace EventSystem.Pages;

public partial class SelfInvite_Page : ContentPage
{
    private Happening happening;
	public SelfInvite_Page(Happening _happening)
	{
		InitializeComponent();
        this.happening = _happening;
        try
        {
            this.happening.Invitees = App.InviteeRepo.GetEntitiesWithChildren().FindAll(x => x.HappeningId == happening.Id);
        }
        catch
        {
            this.happening.Invitees = new List<Invitee>();
        }
    }
	public async void CreateInviteeButton_Pressed(object sender, EventArgs e)
	{
        if (InviteeEmail_Entry.Text != "" && InviteeName_Entry.Text != "")
        {
            if (happening.Invitees.FirstOrDefault(x => x.Email == InviteeEmail_Entry.Text) == null)
            {
                Invitee invitee = new Invitee(InviteeName_Entry.Text, InviteeEmail_Entry.Text, this.happening.Id);
                invitee.Happening = happening;
                App.InviteeRepo.SaveEntity(invitee);
                await DisplayAlert("Succes", "Uitnodiging is succesvol gestuurd", "Laat volgende zich uitnodigen");
                this.InviteeEmail_Entry.Placeholder = "Typ hier je e-mail";
                this.InviteeName_Entry.Placeholder = "Typ hier je naam";
                this.InviteeName_Entry.Text = "";
                this.InviteeEmail_Entry.Text = "";

                
            }
            else
            {
                InviteeEmail_Entry.Text = "";
                InviteeEmail_Entry.Placeholder = "Er is al een gast met deze email uitgenodigd.";
            }
        }
    }
}