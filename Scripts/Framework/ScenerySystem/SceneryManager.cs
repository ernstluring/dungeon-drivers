using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneryManager : MonoBehaviour {

	[SerializeField]
	private Transform startBoardPrefab;

	[SerializeField]
	private Transform[] basisBoardPrefabs, variationBoardPrefabs;
	[SerializeField]
	private int numberOfBoards;
	[SerializeField]
	private const float POOL_Z_POSITION = -300;
	
	private const float SPAWNOFFSET = 0f;
	private const float SPAWNOFFSET_SPECIALBLOCK = 0.6f;
	private const float RECYCLEOFFSET = -120;
	
	private Transform[] firstToInstantiatePrefabs;

	private Vector3 startPosition = new Vector3(0, 0, 0);

	private SceneryGenerator sceneryGenerator;
	
	void Start () {
		firstToInstantiatePrefabs = basisBoardPrefabs;

		sceneryGenerator = new SceneryGenerator(numberOfBoards, SPAWNOFFSET, SPAWNOFFSET_SPECIALBLOCK, POOL_Z_POSITION, RECYCLEOFFSET);

		SetupPool(variationBoardPrefabs);

		BoardSetup();
		
		StartCoroutine(SpawnBlock());
	}

	private void SetupPool (Transform[] objects) {
		for (int i = 0; i < objects.Length; i++) {
			Transform obj = (Transform)Instantiate(objects[i]);
			sceneryGenerator.SetPoolQueue(obj);
		}
	}

	private void BoardSetup () {
		List<Transform> objects = new List<Transform>();
		Transform obj;
		for (int i = 0; i < numberOfBoards; i++) {
			obj = (Transform)Instantiate(firstToInstantiatePrefabs[i]);
			objects.Add(obj);
		}
		sceneryGenerator.BoardSetup(startPosition, objects);
	}

	void Update () {

		if (sceneryGenerator.CanBeGenerated()) {
			sceneryGenerator.Generate();
		}

		if (startBoardPrefab.position.z < -200)
			startBoardPrefab.gameObject.SetActive(false);
	}

	IEnumerator SpawnBlock () {
		while (true) {
			sceneryGenerator.AddSpecialToQueue();
			yield return new WaitForSeconds(Random.Range(5, 10));
		}
	}

}
