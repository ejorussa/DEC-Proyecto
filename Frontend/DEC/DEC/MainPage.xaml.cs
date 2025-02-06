

namespace DEC;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	public async void BtnInfo_Clicked(object sender, EventArgs e)
	{
		await DisplayAlert("Manual de usuario", "", "OK");
	}

	public async void BtnPreg_Clicked(object sender, EventArgs e)
	{
		string mensaje = "Hola, somos una grupo de estudiantes de la carrera Ingeniería en Sistemas de la Universidad Tecnológica Nacional FRC";
		await DisplayAlert("¿Quienes somos?",mensaje,"Volver");
	}
}

