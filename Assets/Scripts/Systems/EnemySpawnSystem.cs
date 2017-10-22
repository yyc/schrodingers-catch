﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;

public class EnemySpawnSystem : MonoBehaviour {
  int currentEnemies = 0;
  public GameObject enemyPrefab;

  // Use this for initialization
  void Start() {}

  // Update is called once per frame
  void Update() {
    if (currentEnemies == 0) {
      Spawn(new int[1] {
        2
      });
    }
  }

  void Spawn(int[] enemies) {
    for (int i = 0; i < enemies.Length; i++) {
      for (int j = 0; j < enemies[i]; j++) {
        GameObject newEnemy = Instantiate(enemyPrefab,
                                          Vector3.zero,
                                          Quaternion.identity);

        MemoryComponent memComponent = newEnemy.GetComponent<MemoryComponent>();

        memComponent.position = randomValidPosition();
        currentEnemies++;
      }
    }
  }

  Tuple3I randomValidPosition() {
    Tuple3I position = Tuple3I.zero;

    do {
      int mapRow = Random.Range(0, MapGenerator.numRows);
      int mapCol = Random.Range(0, MapGenerator.numCols);
      position = new Tuple3I(mapRow, mapCol, Random.Range(0, 4));
    } while (!MapGenerator.isValidPosition(position));
    return position;
  }
}
