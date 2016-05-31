using System;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	[System.Serializable]
	public class GridInformation {
		[SerializeField]
		private float gridHeight = 3.5f;
		[SerializeField]
		private float gridWidth = 2.7f;

		public float GridHeight {
			get { return gridHeight; }
		}
		public float GridWidth {
			get { return gridWidth; }
		}
	}
	[System.Serializable]
	public class AttackParticles {
		public ParticleSystem explosionParticle;
		public GameObject explosionObject;
		public ParticleSystem fire;

	}

	public GridInformation gridInfo;
	public AttackParticles attackParticles;

	public static readonly System.Random random = new System.Random ();
}
