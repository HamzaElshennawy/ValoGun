using ValoGun.Models.Maps;

namespace ValoGun.Pages;

public partial class MapPage : ContentPage
{
	public MapPage()
	{
		InitializeComponent();
	}

	private void RotateRight(object sender, EventArgs e)
	{
		IsBusy = true;
		double rotation = MapIconImage.Rotation;
		if(rotation == 360)
		{
			rotation = 0;
			MapIconImage.Rotation = rotation;
		}

		MapIconImage.RotateTo(rotation + 90, 200, Easing.Linear);
		IsBusy = false;
	}
	private void RotateLeft(object sender, EventArgs e)
	{
		IsBusy = true;
		double rotation = MapIconImage.Rotation;
		if(rotation == -360)
		{
			rotation = 0;
			MapIconImage.Rotation = rotation;
		}

		MapIconImage.RotateTo(rotation - 90, 200, Easing.Linear);
		IsBusy = false;
	}
}
