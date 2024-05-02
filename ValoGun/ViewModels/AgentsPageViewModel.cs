using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Newtonsoft.Json;
using ValoGun.Models.Agents;
using ValoGun.Pages;

namespace ValoGun.ViewModels
{
	public partial class AgentsPageViewModel : BaseViewModel
	{
		public ObservableRangeCollection<Data> _AgentsNew { get; set; } = [];
		public ObservableRangeCollection<AgentToView> _AgentsToViewNew { get; set; } = [];

		public AgentsPageViewModel()
		{
			Thread AgentsThread = new(async () => await ReadAgents());
			AgentsThread.Start();
		}

		private async Task ReadAgents()
		{
			await LoadData();
			await RefrshUI();
		}

		private async Task RefrshUI()
		{
			await MainThread.InvokeOnMainThreadAsync(() => OnPropertyChanged(nameof(_AgentsToViewNew)));
		}

		[RelayCommand]
		public async Task NavigateToAgent(AgentToView _agent)
		{
			Data? Agent = _AgentsNew.FirstOrDefault(c => c.uuid == _agent.uuid);
			AgentDetailsViewModel.MainAgent = Agent!;
			await Shell.Current.GoToAsync($"{nameof(AgentDetailsPage)}", true);
		}
		
		public async Task<bool> LoadData()
		{
			try
			{
				using var stream = await FileSystem.OpenAppPackageFileAsync("Agents.json");
				using var reader = new StreamReader(stream);
				var data = await reader.ReadToEndAsync();
				var jsonData = JsonConvert.DeserializeObject<Agents>(data);
				var dataSorted = jsonData!.data.OrderBy(x => x.displayName).ToList();
				_AgentsNew.Clear();

				foreach(var agent in dataSorted)
				{
					string lowerName = agent.displayName.ToLower();
					if(agent.displayName.Equals("KAY/O", StringComparison.OrdinalIgnoreCase))
					{
						lowerName = "kayo";
					}
					for(int i = 0; i < agent.abilities.Length; i++)
					{
						agent.abilities[i].DisplayIcon = $"{lowerName}displayicon{i + 1}.png";
					}
					agent.portrait = $"{lowerName}fullportrait.png";
					agent.Background = $"{lowerName}background.png";
					_AgentsNew.Add(agent);
					_AgentsToViewNew.Add(new AgentToView
					{
						uuid = agent.uuid,
						displayName = agent.displayName,
						displayIcon = agent.displayIcon,
						background = agent.background,
						role = agent.role.displayName,
						fullPortrait = agent.fullPortrait
					});
				}
			}
			catch(Exception)
			{
				return false;
			}

			return true;
		}
	}
}
