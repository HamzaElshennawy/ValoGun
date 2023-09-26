using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Newtonsoft.Json;
using ValoGun.Models.Agents;
using ValoGun.Pages;

namespace ValoGun.ViewModels
{
	public partial class AgentsPageViewModel : BaseViewModel
	{
		public Agents agents { get; set; }

		public ObservableRangeCollection<Data> _Agents { get; set; }
		private ObservableRangeCollection<Data> AgentsLoading;

		public AgentsPageViewModel()
		{
			_Agents = [];
			AgentsLoading = [];
			//Thread ReadAgentsThread = new Thread(async () => await ReadAgents());
			//ReadAgentsThread.Start();
			Task.Run(ReadAgents);
		}

		private async Task ReadAgents()
		{
			using var stream = await FileSystem.OpenAppPackageFileAsync("Agents.json");
			using var reader = new StreamReader(stream);
			var data = reader.ReadToEnd();
			var jsonData = JsonConvert.DeserializeObject<Agents>(data);
			_Agents.Clear();
			var dataSorted = jsonData.data.OrderBy(x => x.displayName);
			//foreach (var agent in dataSorted)
			//{
			//	//string lowerName = agent.displayName.ToLower();
			//	//if (agent.displayName == "KAY/O")
			//	//{
			//	//	lowerName = "kayo";
			//	//}
			//	//for (int i = 0; i < agent.abilities.Length; i++)
			//	//{
			//	//	agent.abilities[i].DisplayIcon = $"{lowerName}displayicon{i + 1}.png";
			//	//}
			//	//agent.portrait = $"{lowerName}fullportrait.png";
			//	//agent.Background = $"{lowerName}background.png";
			//	//AgentsLoading.Add(agent);
			//	AgentsLoading.Add(agent);
			//}
			_Agents.AddRange(dataSorted);
			//await MainThread.InvokeOnMainThreadAsync(() => _Agents = AgentsLoading);
			//await MainThread.InvokeOnMainThreadAsync(() => OnPropertyChanged(nameof(_Agents)));
			//return Task.CompletedTask;
		}


		[RelayCommand]
		public async Task NavigateToAgent(Data agent)
		{
			AgentDetailsViewModel.MainAgent = agent;
			await Shell.Current.GoToAsync($"{nameof(AgentDetailsPage)}", true);
		}
	}
}
