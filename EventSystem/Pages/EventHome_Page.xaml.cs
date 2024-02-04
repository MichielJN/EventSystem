using EventSystem.Models;
using EventSystem.Data;
using IronQr;
using IronSoftware.Drawing;
using System.Net;
using System.Net.Mail;

namespace EventSystem.Pages;

public partial class EventHome_Page : ContentPage
{
	private EventDB db = new EventDB();
	private Happening happening;
	public EventHome_Page(Happening _happening)
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
		
		this.InviteesNotInEvent_Grid.ItemsSource = happening.GetInviteesNotInHappening();
		this.InviteesInEvent_Grid.ItemsSource = happening.GetInviteesInHappening();	
	}

	public async void BackButton_Pressed(object sender, EventArgs e)
	{
		//await Application.Current.MainPage.Navigation.PushModalAsync(new EventMakerHome_Page(this.happening.HappeningMaker));
        await Application.Current.MainPage.Navigation.PushModalAsync(new EventMakerHome_Page(App.HappeningMakerRepo.GetEntityWithChildren(this.happening.HappeningMaker.Id)));
    }


	public async void ReadQRButton_Pressed(object sender, EventArgs e)
	{

	}

	public async void ManageInviteeButton_Pressed(object sender, EventArgs e)
	{
		Application.Current.MainPage.Navigation.PushModalAsync(new ManageInvitee_Page(App.InviteeRepo.GetEntitiesWithChildren().FirstOrDefault(x => x.Name == ((Button)sender).Text)));
	}

    public async void CreateInviteeButton_Pressed(object sender, EventArgs e)
	{
		if (InviteeEmail_Entry.Text != "" && InviteeName_Entry.Text != "")
		{
			if (happening.Invitees.FirstOrDefault(x => x.Email == InviteeEmail_Entry.Text) == null)
			{
				Invitee invitee = new Invitee(InviteeName_Entry.Text, InviteeEmail_Entry.Text, this.happening.Id);
				invitee.Happening = happening;
				//db.SaveOrUpdateInvitee(invitee);
				App.InviteeRepo.SaveEntity(invitee);
				//qr code sturen
				//QrCode inviteeQR = QrWriter.Write(invitee.GetQRStringInvitee());
				//AnyBitmap qrImage = inviteeQR.Save();
				//qrImage.SaveAs(Path.Combine(FileSystem.AppDataDirectory, "QRInvitee.png"));
				//email maken

				//qr code deleten na versturen email
				//  File.Delete(Path.Combine(FileSystem.AppDataDirectory, "QRInvitee.png"));
				await Application.Current.MainPage.Navigation.PushModalAsync(new EventHome_Page(App.HappeningRepo.GetEntityWithChildren(this.happening.Id)));
			}
			else
			{
				InviteeEmail_Entry.Text = "";
				InviteeEmail_Entry.Placeholder = "Er is al een gast met deze email uitgenodigd.";
			}
		}
	}
}