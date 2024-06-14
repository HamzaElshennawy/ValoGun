using MvvmHelpers;
using ValoGun.Models.Maps;
namespace ValoGun.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
		public static Data Map { get; set; }

		public Data MainMap { get; set; }
		public MapViewModel()
		{
			MainMap = Map;
			Title = MainMap.displayName;
		}
	}
}
