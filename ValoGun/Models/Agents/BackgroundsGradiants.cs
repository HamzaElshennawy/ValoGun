namespace ValoGun.Models.Agents;

public class Gradiants
{
	public Backgroundsgradiantscolor backgroundsGradiantsColors { get; set; }
}

public class Backgroundsgradiantscolor
{
	public Backgroundgradient backgroundGradient { get; set; }
	public Offsets Offsets { get; set; }
}

public class Backgroundgradient
{
	public string GradientColor1 { get; set; }
	public string GradientColor2 { get; set; }
	public string GradientColor3 { get; set; }
	public string GradientColor4 { get; set; }
}

public class Offsets
{
	public string Offset1 { get; set; }
	public string Offset2 { get; set; }
	public string Offset3 { get; set; }
	public string Offset4 { get; set; }
}
