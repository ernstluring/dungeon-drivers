using UnityEngine;
using System.Collections;

public class TextureOffset : MonoBehaviour {

	[SerializeField]
	private float scrollSpeed = 1f;
	private Renderer rend;

	private Vector2 offset;

	void Start () {
		rend = GetComponent<Renderer>();
	}
	
	void Update () {
		offset = new Vector2 (0, Time.time * scrollSpeed);

		rend.material.SetTextureOffset("_MainTex", -offset);
	}
}
