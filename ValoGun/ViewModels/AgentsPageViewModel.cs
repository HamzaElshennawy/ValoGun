using MvvmHelpers;
using Newtonsoft.Json;
using ValoGun.Models.Agents;

namespace ValoGun.ViewModels
{
	public class AgentsPageViewModel : BaseViewModel
	{
		public Agents agents { get; set; }

		public ObservableRangeCollection<Data> _Agents { get; set; }
		private ObservableRangeCollection<Data> AgentsLoading;

		public AgentsPageViewModel()
		{
			_Agents = [];
			AgentsLoading = [];
			Thread ReadAgentsThread = new Thread(async ()=> await ReadAgents());
			ReadAgentsThread.Start();
		}

		private async Task ReadAgents()
		{
			using var stream = await FileSystem.OpenAppPackageFileAsync("Agents.json");
			using var reader = new StreamReader(stream);

			var data = reader.ReadToEnd();
			var jsonData = JsonConvert.DeserializeObject<Agents>(data);
			var dataSorted = jsonData.data.OrderBy(x => x.displayName);
			foreach(var agent in dataSorted)
			{
				AgentsLoading.Add(agent);
			}
			await MainThread.InvokeOnMainThreadAsync(() => _Agents = AgentsLoading);
			//await MainThread.InvokeOnMainThreadAsync(() => _Agents.OrderBy(x => x.displayName));
			await MainThread.InvokeOnMainThreadAsync(()=> OnPropertyChanged(nameof(_Agents)));
		}
	}
}
