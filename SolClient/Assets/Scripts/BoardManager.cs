using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System.Collections;

public class BoardManager : MonoBehaviour {

	[Serializable]
	public class Count
	{
		public int minimum;
		public int maximum;

		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}

	}

	public int columns = 8;
	public int rows = 8;
	// collidable walls
	public Count wallCount = new Count(5, 9);
	public Count foodCount = new Count(1,5);
	public GameObject exit;
	public GameObject[] floorTiles;
	public GameObject[] enemyTiles;
	public GameObject[] foodTiles;
	public GameObject[] wallTiles;
	public GameObject[] outerWallTiles;

	private Transform boardHolder;
	private List<Vector3> gridPositions = new List<Vector3>();

	void initialiseList ()
	{
		gridPositions.Clear ();
		for (int x = 0; x < columns -1; x++)
		{
			for (int y = 0; y < rows - 1; y++)
			{
				gridPositions.Add (new Vector3(x, y, 0f));

			}
		}
	}

	void boardSetup()
	{
		boardHolder = new GameObject ("Board").transform;
		for (int x = -1; x < columns; x++) {
			for (int y = -1; y < rows; y++)
			{
				GameObject toInstantiate = floorTiles[Random.Range (0, floorTiles.Length)];
				if (x == -1 || x == columns || y == -1 || y == columns)
				{
					toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
				}
				GameObject instance = Instantiate(toInstantiate, new Vector3(x,y,0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
			}
		}
	}

	Vector3 randomPosition()
	{
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions[randomIndex];
		gridPositions.RemoveAt (randomIndex);
		return randomPosition;

	}

	void layoutObjectRandom(GameObject[] tileArray, int min, int max)
	{
		int objectCount = Random.Range (min, max - 1);
		for (int i = 0; i < objectCount; i++) {
			Vector3 randomPosition = randomPosition ();
			GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
			Instantiate (tileChoice, randomPosition, Quaternion.identity);
		}
	}

	void SetupScene(int level)
	{
		boardSetup ();
		initialiseList ();
		layoutObjectRandom (wallTiles, wallCount.minimum, wallCount.maximum);
		layoutObjectRandom (foodTiles, foodCount.minimum, foodCount.maximum);
		int enemyCount = (int)Mathf.Log (level, 2f);
		layoutObjectRandom (enemyTiles, enemyCount, enemyCount);
		Instantiate (exit, new Vector3 (columns - 1, rows - 1, 0f), Quaternion.identity);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
