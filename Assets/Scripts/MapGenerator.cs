using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;

public class MapGenerator : MonoBehaviour {

	int[,] map;
	public Sprite[] sprites;
	public GameObject tilePrefab;
	public GameObject character;

	private static float tileHeight = 0, tileWidth = 0;

	// Use this for initialization
	void Start () {
		// Read in maze
		string fileData = System.IO.File.ReadAllText ("maze.csv");
		string[] rows = fileData.Split("\n"[0]);
		string[] cols = rows[0].Split ("," [0]);
		int numRows = rows.Length, numCols = cols.Length;
		map = new int[numRows, numCols];
		for (int i = 0; i < numRows; i++) {
			cols = rows [i].Split ("," [0]);
			for(int j = 0; j < numCols; j++){
				int.TryParse(cols[j], out map[i, j]);
			}
		}

		// Generate Maze
		tileHeight = tilePrefab.transform.localScale.y;
		tileWidth = tileHeight; // make it square for now

		Tuple3<int, int, int> charCoords = new Tuple3<int, int, int> (0, 0,0);

		for (int i = 0; i < numRows; i++) {
			for (int j = 0; j < numCols; j++) {
				Vector3 position = PositionFor (i, j);

				GameObject newTile = Instantiate (tilePrefab, position, Quaternion.identity);
				newTile.GetComponent<SpriteRenderer> ().sprite = sprites [map [i, j]];
				if(map[i, j] == 2){ // Store portal location
					charCoords = new Tuple3<int, int, int> (i, j, 0);
				}
			}
		}

		character.transform.position = PositionFor(charCoords);
		character.GetComponent<CompletePlayerController> ().storedPosition = charCoords;
	
	}

	public static Vector3 PositionFor(Tuple3<int, int, int> tuple, float z = 0) {
		return PositionFor (tuple.first, tuple.second, z);
	}


	public static Vector3 PositionFor(Tuple<int, int> tuple, float z = 0) {
		return PositionFor (tuple.first, tuple.second, z);
	}

	public static Vector3 PositionFor(int row, int col, float z = 0) {
		return new Vector3 (col * tileHeight, row * tileWidth, z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
