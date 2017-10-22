using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;

public class MapGenerator : MonoBehaviour {
  private static int[,] map, pathMap;
  public Sprite[] sprites;
  public GameObject tilePrefab;
  public GameObject character;
  public static string maze_filename = "maze.csv";
  public static List<TupleI>portalLocations = new List<TupleI>();
  public static int numRows, numCols;


  private static float tileHeight = 0, tileWidth = 0, tileScale = 0;
  private static Vector3 mapOrigin = Vector3.zero;

  // Use this for initialization
  void Start() {
    mapOrigin = transform.position;

    // Read in maze
    string fileData = System.IO.File.ReadAllText("maze.csv");

    string[] rows = fileData.Split("\n"[0]);
    string[] cols = rows[0].Split(","[0]);
    numRows = rows.Length;
    numCols = cols.Length;
    map     = new int[numRows, numCols];
    pathMap = new int[numRows, numCols];

    for (int i = 0; i < numRows; i++) {
      cols = rows[i].Split(","[0]);

      for (int j = 0; j < numCols; j++) {
        int.TryParse(cols[j], out map[i, j]);
      }
    }

    // Generate Maze
    tileHeight = tilePrefab.GetComponent<BoxCollider2D>().size.y;
    tileWidth  = tileHeight; // make it square for now
    tileScale  = GetComponent<BoxCollider2D>().size.y /
                 (tileHeight * numRows);
    tileHeight *= tileScale;
    tileWidth  *= tileScale;

    tilePrefab.transform.localScale = new Vector3(tileScale, tileScale, 1);


    for (int i = 0; i < numRows; i++) {
      for (int j = 0; j < numCols; j++) {
        Vector3 position = TilePositionFor(i, j);

        GameObject newTile =
          Instantiate(tilePrefab, position, Quaternion.identity, transform);
        newTile.GetComponent<SpriteRenderer> ().sprite = sprites[map[i, j]];

        tilePrefab.transform.localScale = new Vector3(tileScale, tileScale, 1);

        if (map[i, j] == 2) { // Store portal location
          portalLocations.Add(new TupleI(i, j));
        }
      }
    }

    TupleI charCoords = portalLocations[0];
    character.transform.position = PositionFor(charCoords);

    character.GetComponent<MemoryComponent> ().position = new Tuple3I(
      charCoords.first,
      charCoords.second,
      0);

    GeneratePathfindingMap();

    // For debugging pathmap
    // for (int i = 0; i < numRows; i++) {
    //   String result = "";
    //
    //   for (int j = 0; j < numCols; j++) {
    //     result += pathMap[i, j] + " ";
    //   }
    //   Debug.Log(result);
    // }
  }

  private Vector3 TilePositionFor(int row, int col, float z = 0) {
    return mapOrigin + new Vector3(col * tileHeight, row * tileWidth, z);
  }

  public static Vector3 PositionFor(Tuple3I tuple, float z = 0) {
    return PositionFor(tuple.first, tuple.second, z);
  }

  public static Vector3 PositionFor(TupleI tuple, float z = 0) {
    return PositionFor(tuple.first, tuple.second, z);
  }

  public static Vector3 PositionFor(int row, int col, float z = 0) {
    return mapOrigin + new Vector3(col * tileHeight, row * tileWidth, z);
  }

  public static bool isValidPosition(Tuple3I position) {
    return isValidPosition(position.first, position.second);
  }

  public static bool isValidPosition(TupleI position) {
    return isValidPosition(position.first, position.second);
  }

  public static bool isValidPosition(int row, int col) {
    // if it's out of bounds
    if ((row < 0) || (row >= map.GetLength(0)) ||
        (col < 0) || (col >= map.GetLength(1))) {
      return false;
    }

    // wall
    if (map[row, col] == 1) {
      return false;
    }
    return true;
  }

  private void GeneratePathfindingMap() {
    Queue<TupleI> currentFrontier;
    Queue<TupleI> nextFrontier = new Queue<TupleI>(portalLocations);
    TupleI   position, nextPosition;
    TupleI[] transforms = new TupleI[4] {
      new TupleI(1, 0),
      new TupleI(0, 1),
      new TupleI(-1, 0),
      new TupleI(0, -1)
    };
    int index = 1;

    foreach (TupleI temp in portalLocations) {
      pathMap[temp.first, temp.second] = 1;
    }

    while (nextFrontier.Count != 0) {
      currentFrontier = nextFrontier;
      nextFrontier    = new Queue<TupleI>();

      while (currentFrontier.Count != 0) {
        position = currentFrontier.Dequeue();

        //        pathMap[position.first, position.second] = index;

        for (int i = 0; i < transforms.Length; i++) {
          nextPosition = position + transforms[i];

          if (!isValidPosition(nextPosition)) {
            continue;
          }

          if (pathMap[nextPosition.first, nextPosition.second] == 0) {
            pathMap[nextPosition.first, nextPosition.second] = index + 1;
            nextFrontier.Enqueue(nextPosition);
          }
        }
      }
      index++;
    }
  }

  public static int GetPathValueFor(Tuple3I position) {
    return GetPathValueFor(position.first, position.second);
  }

  public static int GetPathValueFor(int row, int col) {
    return pathMap[row, col];
  }
}
