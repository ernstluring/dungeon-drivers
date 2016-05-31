using UnityEngine;
using System.Collections.Generic;

public class SceneryGenerator {
	
	private Queue<Transform> objectQueue;

	private Queue<Transform> poolQueue;

	private float spawnOffset, spawnOffsetSpecialBlock;

	private Vector3 poolPosition;
	private float poolZPosition;
	
	private float boardRendererSize;

	private float recycleOffset;

	private Transform lastObjectInQueue;
	

	public SceneryGenerator (int numberOfBoards, float spawnOffset, float spawnOffsetSpecialBlock, float poolZPosition, float recycleOffset) {
		objectQueue = new Queue<Transform>(numberOfBoards);
		poolQueue = new Queue<Transform>();
		this.spawnOffset = spawnOffset;
		this.spawnOffsetSpecialBlock = spawnOffsetSpecialBlock;
		this.poolZPosition = poolZPosition;
		this.recycleOffset = recycleOffset;
	}

	public void BoardSetup (Vector3 startPosition, List<Transform> objects) {
		Vector3 tempPos;
		Vector3 nextPosition = startPosition;
		for (int i = 0; i < objects.Count; i++) {
			tempPos = objects[i].position;
			tempPos.z = nextPosition.z;
			objects[i].position = tempPos;

			boardRendererSize = objects[i].GetComponent<Renderer>().bounds.size.z;
			nextPosition.z += boardRendererSize - spawnOffset;

			lastObjectInQueue = objects[i];
			objectQueue.Enqueue(objects[i]);
		}
	}

	public void  SetPoolQueue (Transform obj) {
		poolPosition = obj.position;
		poolPosition.z = poolZPosition;
		obj.position = poolPosition;

		obj.GetComponent<Board>().enabled = false;
		obj.gameObject.SetActive(false);

		// Add object to Queue
		poolQueue.Enqueue(obj);

	}

	public void AddSpecialToQueue () {

		Transform obj = poolQueue.Dequeue();
		obj.gameObject.SetActive(true);
		obj.GetComponent<Board>().enabled = true;
		Vector3 tempPos = obj.position;
		tempPos.z = lastObjectInQueue.position.z + boardRendererSize - spawnOffsetSpecialBlock;
		obj.position = tempPos;
		
		lastObjectInQueue = obj;
		boardRendererSize = lastObjectInQueue.GetComponent<Renderer>().bounds.size.z;

		objectQueue.Enqueue(obj);

	}
	

	public bool CanBeGenerated () {
		return objectQueue.Peek().position.z < recycleOffset;
	}

	public void Generate () {

		Transform obj = objectQueue.Dequeue();

		if (obj.GetComponent(typeof(VariationBoard))) {
			obj.gameObject.SetActive(false);
			obj.GetComponent<VariationBoard>().enabled = false;
			obj.position = poolPosition;
			poolQueue.Enqueue(obj);
		} else {
			Vector3 tempPos = obj.position;
			tempPos.z = lastObjectInQueue.position.z + boardRendererSize - spawnOffset;
			obj.position = tempPos;

			objectQueue.Enqueue(obj);
			lastObjectInQueue = obj;
		}

		boardRendererSize = lastObjectInQueue.GetComponent<Renderer>().bounds.size.z;
	}

}