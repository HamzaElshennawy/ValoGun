namespace ValoGun.Models.Weapons
{
        public class Shopdata
        {
            public int cost { get; set; }
            public string category { get; set; }
            public string categoryText { get; set; }
            public Gridposition gridPosition { get; set; }
            public bool canBeTrashed { get; set; }
            public object image { get; set; }
            public string newImage { get; set; }
            public object newImage2 { get; set; }
            public string assetPath { get; set; }
        }

    }
