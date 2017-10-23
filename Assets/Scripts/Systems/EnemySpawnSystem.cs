﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;

public class EnemySpawnSystem : MonoBehaviour {
  public int currentEnemies = 0;
  public GameObject enemyPrefab;

  int globalCount = 0;

  // Minimum distance from a portal
  public int minimumSpawnDistance = 10;

  // Use this for initialization
  void        Start() {}

  public void Despawned(GameObject enemy) {
    Debug.Log("despawned " + enemy.name);
    currentEnemies--;
  }

  // Update is called once per frame
  void Update() {
    if (currentEnemies == 0) {
      Debug.Log("Spawning More!");
      Spawn(new int[1] {
        2
      });
    }
  }

  void Spawn(int[] enemies) {
    Timekeeper timekeeper = Timekeeper.getInstance();

    Debug.Log("starting spawn" + currentEnemies);

    for (int i = 0; i < enemies.Length; i++) {
      GameObject[] twins = new GameObject[enemies[i]];
      TupleI observed    = new TupleI(0, enemies[i]);

      for (int j = 0; j < enemies[i]; j++) {
        currentEnemies++;

        GameObject newEnemy = Instantiate(enemyPrefab,
                                          Vector3.zero,
                                          Quaternion.identity);

        twins[j]      = newEnemy;
        newEnemy.name = "Enemy" + (globalCount++);

        newEnemy.GetComponent<MemoryComponent>().position =
          randomValidPosition();
        newEnemy.GetComponent<MemoryComponent>().firstActiveTick =
          timekeeper.getTick();

        EnemyObservingSystem observer =
          newEnemy.GetComponent<EnemyObservingSystem>();
        observer.twins       = twins;
        observer.observed    = observed;
        observer.spawnSystem = this;
      }
    }
    Debug.Log("Finished spawn" + currentEnemies);
  }

  Tuple3I randomValidPosition() {
    Tuple3I position = Tuple3I.zero;

    do {
      int mapRow = Random.Range(0, MapGenerator.numRows);
      int mapCol = Random.Range(0, MapGenerator.numCols);
      position = new Tuple3I(mapRow, mapCol, Random.Range(0, 4));
    } while (!MapGenerator.isValidPosition(position) ||
             MapGenerator.GetPathValueFor(position) < minimumSpawnDistance);
    return position;
  }
}
