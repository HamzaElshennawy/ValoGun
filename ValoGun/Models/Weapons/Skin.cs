namespace ValoGun.Models.Weapons
{
        public class Skin
        {
            public string uuid { get; set; }
            public string displayName { get; set; }
            public string themeUuid { get; set; }
            public string contentTierUuid { get; set; }
            public string displayIcon { get; set; }
            public string wallpaper { get; set; }
            public string assetPath { get; set; }
            public Chroma[] chromas { get; set; }
            public Level[] levels { get; set; }
        }

    }
