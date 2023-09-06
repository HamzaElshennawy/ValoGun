namespace ValoGun.Models
{
    public partial class Weapons
    {
        public class Datum
        {
            public string uuid { get; set; }
            public string displayName { get; set; }
            public string category { get; set; }
            public string defaultSkinUuid { get; set; }
            public string displayIcon { get; set; }
            public string killStreamIcon { get; set; }
            public string assetPath { get; set; }
            public Weaponstats weaponStats { get; set; }
            public Shopdata shopData { get; set; }
            public Skin[] skins { get; set; }
        }

    }
}
