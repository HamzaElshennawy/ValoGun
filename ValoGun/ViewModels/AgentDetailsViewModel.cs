using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoGun.Models.Agents;
using Microsoft.Maui.ApplicationModel;

namespace ValoGun.ViewModels
{
	public class AgentDetailsViewModel : BaseViewModel
	{

		public static Data MainAgent { get; set; }
		public AgentDetailsViewModel()
		{
			MainThread.InvokeOnMainThreadAsync(()=>OnPropertyChanged(nameof(MainAgent)));
		}
	}
}
