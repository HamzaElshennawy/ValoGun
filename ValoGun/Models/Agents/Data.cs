﻿namespace ValoGun.Models.Agents
{
	public class Data
	{
		public string uuid { get; set; }
		public string displayName { get; set; }
		public string displayIcon { get; set; }
		public string description { get; set; }
		public string developerName { get; set; }
		public string[] characterTags { get; set; }
		public string fullPortrait { get; set; }
		public string portrait { get; set; }
		public string Background { get; set; }
		public string background { get; set; }
		public Gradiants _backgroundGradientColors { get; set; }
		public string[] backgroundGradientColors { get; set; }
		public bool isFullPortraitRightFacing { get; set; }
		public bool isPlayableCharacter { get; set; }
		public bool isAvailableForTest { get; set; }
		public bool isBaseContent { get; set; }
		public Role role { get; set; }
		public Recruitmentdata recruitmentData { get; set; }
		public Ability[] abilities { get; set; }
		public Voiceline voiceLine { get; set; }
	}

}

