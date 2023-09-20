using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoGun.Models.Agents;

namespace ValoGun.ViewModels
{
	public class AgentDetailsViewModel : BaseViewModel
	{

		public static Data MainAgent { get; set; }
		public Data BindableAgent { get; set; }
		public AgentDetailsViewModel()
		{
			BindableAgent = MainAgent;
			MainThread.BeginInvokeOnMainThread(()=> OnPropertyChanged(nameof(BindableAgent)));
		}
	}
}
