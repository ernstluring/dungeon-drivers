using UnityEngine;
using System.Collections;

public class ScreenResolution : MonoBehaviour {

	[SerializeField]
	private int screenWidth = 3840;
	[SerializeField]
	private int screenHeight = 1080;

	void Start () {
		Screen.SetResolution (screenWidth, screenHeight, false);
	}
}
