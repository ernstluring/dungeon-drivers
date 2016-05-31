using UnityEngine;
using System.Collections;

public enum TileType {
	TILE,
	PULLPOSLEFT,
	PULLPOSMIDDLE,
	PULLPOSRIGHT,
	BACKLEFT,
	BACKMIDDLE,
	BACKRIGHT,
	DITCHLEFTFRONT,
	DITCHLEFTMIDDLE,
	DITCHLEFTBACK,
	DITCHRIGHTFRONT,
	DITCHRIGHTMIDDLE,
	DITCHRIGHTBACK
}

public class GridTile : MonoBehaviour {
	[SerializeField]
	private TileType tileType;

	public TileType GetTileType {
		get { return tileType; }
	}
}
