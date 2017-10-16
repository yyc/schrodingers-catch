using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	int[,] map;
	public Sprite[] sprites;
	public GameObject tilePrefab;
	public GameObject character;

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
		float tileHeight = tilePrefab.transform.localScale.y;
		float tileWidth = tileHeight; // make it square for now

		Vector3 charStart = new Vector3(0,0,0);

		for (int i = 0; i < numRows; i++) {
			for (int j = 0; j < numCols; j++) {
				Vector3 position = new Vector3 (j * tileHeight, i * tileWidth, 0);

				GameObject newTile = Instantiate (tilePrefab, position, Quaternion.identity);
				newTile.GetComponent<SpriteRenderer> ().sprite = sprites [map [i, j]];
				if(map[i, j] == 2){ // Store portal location
					charStart = position;
				}
			}
		}

		character.transform.position = charStart;

	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
