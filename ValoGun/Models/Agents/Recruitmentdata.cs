namespace ValoGun.Models.Agents
{
	public class Recruitmentdata
	{
		public string counterId { get; set; }
		public string milestoneId { get; set; }
		public int milestoneThreshold { get; set; }
		public bool useLevelVpCostOverride { get; set; }
		public int levelVpCostOverride { get; set; }
		public DateTime startDate { get; set; }
		public DateTime endDate { get; set; }
	}

}
