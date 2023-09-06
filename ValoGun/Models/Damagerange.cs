namespace ValoGun.Models
{
    public partial class Weapons
    {
        public class Damagerange
        {
            public int rangeStartMeters { get; set; }
            public int rangeEndMeters { get; set; }
            public float headDamage { get; set; }
            public int bodyDamage { get; set; }
            public float legDamage { get; set; }
        }

    }
}
