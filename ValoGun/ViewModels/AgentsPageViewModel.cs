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
		public ObservableRangeCollection<Grouping<string, Data>> GAgents { get; set; } = new();
		public AgentsPageViewModel()
		{
			_Agents = [];
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
			foreach (var agent in dataSorted)
			{
				string lowerName = agent.displayName.ToLower();
				if (agent.displayName == "KAY/O")
				{
					lowerName = "kayo";
				}
				for (int i = 0; i < agent.abilities.Length; i++)
				{
					agent.abilities[i].DisplayIcon = $"{lowerName}displayicon{i + 1}.png";
				}
				agent.portrait = $"{lowerName}fullportrait.png";
				agent.Background = $"{lowerName}background.png";
				_Agents.Add(agent);
				//AgentsLoading.Add(agent);
			}
			GAgents.Add(new Grouping<string, Data>("Controllers", _Agents.Where(c => c.role.displayName == "Controller").OrderBy((w) => w.displayName)));
			GAgents.Add(new Grouping<string, Data>("Duelists", _Agents.Where(c => c.role.displayName == "Duelist").OrderBy((w) => w.displayName)));
			GAgents.Add(new Grouping<string, Data>("Sentinels", _Agents.Where(c => c.role.displayName == "Sentinel").OrderBy((w) => w.displayName)));
			GAgents.Add(new Grouping<string, Data>("Initiators", _Agents.Where(c => c.role.displayName == "Initiator").OrderBy((w) => w.displayName)));
			OnPropertyChanged(nameof(_Agents));
			OnPropertyChanged(nameof(GAgents));
			
		}


		[RelayCommand]
		public async Task NavigateToAgent(Data agent)
		{
			AgentDetailsViewModel.MainAgent = agent;
			await Shell.Current.GoToAsync($"{nameof(AgentDetailsPage)}", true);
		}
	}
}
