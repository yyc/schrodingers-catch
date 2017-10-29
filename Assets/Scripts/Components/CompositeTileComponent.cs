using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeTileComponent : MonoBehaviour {
  public int[] map = new int[9];
  public GameObject[] tileObjects;
  public Sprite[] walledSprites;
  public Sprite wallSprite;
  public Sprite pathSprite;

  // These are for if the adjacent walls are 0, but the corner is 1
  public Sprite[] cornerSprites;

  int[,] corners = new int[4, 3] {
    { 0, 3, 1 },
    { 2, 1, 5 },
    { 6, 7, 3 },
    { 8, 5, 7 }
  };
  int[] edges = new int[4] {
    1, 3, 5, 7
  };

  // Use this for initialization
  void Start()  {
    refreshTiles();
  }

  // Update is called once per frame
  void        Update() {}

  public void refreshTiles() {
    int center = map[4];
    int cornerIndex, leftIndex, rightIndex;

    // If center is 1, then the whole thing is a wall
    if (center == 1) {
      for (int i = 0; i < 9; i++) {
        tileObjects[i].GetComponent<SpriteRenderer>().sprite = wallSprite;
      }
      return;
    } else {
      tileObjects[4].GetComponent<SpriteRenderer>().sprite = pathSprite;
    }

    // Otherwise, it's a path and we need to check the corners
    for (int i = 0; i < 4; i++) {
      cornerIndex = corners[i, 0];
      leftIndex   = corners[i, 1];
      rightIndex  = corners[i, 2];

      // If both sides are walls, this is a curved wall
      if ((map[leftIndex] == 1) && (map[rightIndex] == 1)) {
        tileObjects[cornerIndex].GetComponent<SpriteRenderer>().sprite =
          walledSprites[cornerIndex];
        continue;
      }

      // If not, then it matches whichever side is a wall

      if (map[leftIndex] == 1) {
        tileObjects[cornerIndex].GetComponent<SpriteRenderer>().sprite =
          walledSprites[leftIndex];
        continue;
      }

      if (map[rightIndex] == 1) {
        tileObjects[cornerIndex].GetComponent<SpriteRenderer>().sprite =
          walledSprites[rightIndex];
        continue;
      }

      // If neither side is a wall, put a corner piece if it's 1
      if (map[cornerIndex] == 1) {
        tileObjects[cornerIndex].GetComponent<SpriteRenderer>().sprite =
          cornerSprites[i];
      } else {
        tileObjects[cornerIndex].GetComponent<SpriteRenderer>().sprite =
          pathSprite;
      }
    }

    // Then check the edges{
    foreach (int edgeIndex in edges) {
      if (map[edgeIndex] == 1) {
        tileObjects[edgeIndex].GetComponent<SpriteRenderer>().sprite =
          walledSprites[edgeIndex];
      }
      else {
        tileObjects[edgeIndex].GetComponent<SpriteRenderer>().sprite =
          pathSprite;
      }
    }
  }
}
