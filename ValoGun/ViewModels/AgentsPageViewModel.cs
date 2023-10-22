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
		public ObservableRangeCollection<AgentToView> _AgentsToView { get; set; }
		public ObservableRangeCollection<Grouping<string, Data>> GAgents { get; set; } = new();
		public ObservableRangeCollection<Grouping<string, AgentToView>> GAgentsToView { get; set; } = new();
		public AgentsPageViewModel()
		{
			_Agents = [];
			_AgentsToView = [];
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


				AgentToView tempAgent = new();
				tempAgent.uuid = agent.uuid;
				tempAgent.displayName = agent.displayName;
				tempAgent.displayIcon = agent.displayIcon;
				tempAgent.background = agent.background;
				tempAgent.role = agent.role.displayName;
				_AgentsToView.Add(tempAgent);
				//AgentsLoading.Add(agent);
			}
			//GAgents.Add(new Grouping<string, Data>("Controllers", _Agents.Where(c => c.role.displayName == "Controller").OrderBy((w) => w.displayName)));
			//GAgents.Add(new Grouping<string, Data>("Duelists", _Agents.Where(c => c.role.displayName == "Duelist").OrderBy((w) => w.displayName)));
			//GAgents.Add(new Grouping<string, Data>("Sentinels", _Agents.Where(c => c.role.displayName == "Sentinel").OrderBy((w) => w.displayName)));
			//GAgents.Add(new Grouping<string, Data>("Initiators", _Agents.Where(c => c.role.displayName == "Initiator").OrderBy((w) => w.displayName)));
			GAgentsToView.Add(new Grouping<string, AgentToView>("Controllers", _AgentsToView.Where(c => c.role == "Controller").OrderBy((w) => w.displayName)));
			GAgentsToView.Add(new Grouping<string, AgentToView>("Duelists", _AgentsToView.Where(c => c.role == "Duelist").OrderBy((w) => w.displayName)));
			GAgentsToView.Add(new Grouping<string, AgentToView>("Sentinels", _AgentsToView.Where(c => c.role == "Sentinel").OrderBy((w) => w.displayName)));
			GAgentsToView.Add(new Grouping<string, AgentToView>("Initiators", _AgentsToView.Where(c => c.role == "Initiator").OrderBy((w) => w.displayName)));
			OnPropertyChanged(nameof(_Agents));
			OnPropertyChanged(nameof(GAgents));
			OnPropertyChanged(nameof(_AgentsToView));
			
		}


		[RelayCommand]
		public async Task NavigateToAgent(Data agent)
		{
			AgentDetailsViewModel.MainAgent = agent;
			await Shell.Current.GoToAsync($"{nameof(AgentDetailsPage)}", true);
		}
	}
}
