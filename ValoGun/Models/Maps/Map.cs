using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValoGun.Models.Maps;

public class Map
{
	public Data?[] data { get; set; }
}
public class Data
{
	public string uuid { get; set; }
	public string displayName { get; set; }
	public string narrativeDescription { get; set; }
	public string tacticalDescription { get; set; }
	public string coordinates { get; set; }
	public string displayIcon { get; set; }
	public string listViewIcon { get; set; }
	public string listViewIconTall { get; set; }
	public string splash { get; set; }
	public string stylizedBackgroundImage { get; set; }
	public string premierBackgroundImage { get; set; }
	public string assetPath { get; set; }
	public string mapUrl { get; set; }
	public float xMultiplier { get; set; }
	public float yMultiplier { get; set; }
	public float xScalarToAdd { get; set; }
	public float yScalarToAdd { get; set; }
	public Callout[] callouts { get; set; }
}
