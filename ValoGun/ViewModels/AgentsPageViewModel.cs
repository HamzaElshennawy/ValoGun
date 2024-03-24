using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Newtonsoft.Json;
using ValoGun.Models.Agents;
using ValoGun.Pages;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;
using System.Diagnostics.Metrics;

namespace ValoGun.ViewModels
{
	public partial class AgentsPageViewModel : BaseViewModel
	{
		public Agents agents { get; set; }

		public ObservableRangeCollection<AgentToView> _AgentsToView { get; set; }
		public ObservableRangeCollection<Data> _Agents { get; set; }
		public ObservableRangeCollection<Grouping<string, AgentToView>> GAgentsToView { get; set; } = [];
		public AgentsPageViewModel()
		{
			_AgentsToView = [];
			_Agents = [];
			Thread AgentsThread = new(async () => await ReadAgents());
			AgentsThread.Start();
		}

		private async Task ReadAgents()
		{
			using var stream = await FileSystem.OpenAppPackageFileAsync("Agents.json");
			using var reader = new StreamReader(stream);
			var data = reader.ReadToEnd();
			var jsonData = JsonConvert.DeserializeObject<Agents>(data);
			var dataSorted = jsonData.data.OrderBy(x => x.displayName);
			_Agents.Clear();
			int counter = 0;
			foreach(var agent in dataSorted)
			{
				string lowerName = agent.displayName.ToLower();
				if(agent.displayName == "KAY/O")
				{
					lowerName = "kayo";
				}
				for(int i = 0; i < agent.abilities.Length; i++)
				{
					agent.abilities[i].DisplayIcon = $"{lowerName}displayicon{i + 1}.png";
				}
				agent.portrait = $"{lowerName}fullportrait.png";
				agent.Background = $"{lowerName}background.png";
				_Agents.Add(agent);
				AgentToView tempAgent = new()
				{
					uuid = agent.uuid,
					displayName = agent.displayName,
					displayIcon = agent.displayIcon,
					background = agent.background,
					role = agent.role.displayName
				};
				_AgentsToView.Add(tempAgent);
				counter++;
				//await MainThread.InvokeOnMainThreadAsync(() => Application.Current.MainPage.DisplayAlert("Counter", $"{counter}", "OK"));
			}
			await MainThread.InvokeOnMainThreadAsync(() => GAgentsToView.Add(new Grouping<string, AgentToView>("Controllers", _AgentsToView.Where(c => c.role == "Controller").OrderBy((w) => w.displayName))));
			await MainThread.InvokeOnMainThreadAsync(() => GAgentsToView.Add(new Grouping<string, AgentToView>("Duelists", _AgentsToView.Where(c => c.role == "Duelist").OrderBy((w) => w.displayName))));
			await MainThread.InvokeOnMainThreadAsync(() => GAgentsToView.Add(new Grouping<string, AgentToView>("Sentinels", _AgentsToView.Where(c => c.role == "Sentinel").OrderBy((w) => w.displayName))));
			await MainThread.InvokeOnMainThreadAsync(() => GAgentsToView.Add(new Grouping<string, AgentToView>("Initiators", _AgentsToView.Where(c => c.role == "Initiator").OrderBy((w) => w.displayName))));
			await MainThread.InvokeOnMainThreadAsync(() => OnPropertyChanged(nameof(_AgentsToView)));

		}


		[RelayCommand]
		public async Task NavigateToAgent(AgentToView _agent)
		{
			Data Agent = new();
			Agent = _Agents.Where(c => c.uuid == _agent.uuid).FirstOrDefault();
			AgentDetailsViewModel.MainAgent = Agent;
			await Shell.Current.GoToAsync($"{nameof(AgentDetailsPage)}", true);
		}
	}
}
