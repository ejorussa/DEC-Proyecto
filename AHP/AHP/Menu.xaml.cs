
namespace AHP;

public partial class Menu : ContentPage
{
	public Menu()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new MainPage(new List<AHP>(),int.Parse(criterios.Text),int.Parse(alternativas.Text),0));
    }
}