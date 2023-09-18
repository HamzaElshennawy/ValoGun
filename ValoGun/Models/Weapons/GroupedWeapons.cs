namespace ValoGun.Models.Weapons
{
	public class GroupedWeapons: List<Datum>
	{
		public string Category { get; set; }

		public GroupedWeapons(string category, List<Datum> weapons) : base(weapons)
		{
			Category = category;
		}
	}
}
