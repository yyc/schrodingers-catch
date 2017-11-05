using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;

public class WaveControllerSystem : MonoBehaviour {
  public float secondsBetweenWaves = 3.0f;
  public int wave                  = 0;

  private float nextSpawnTime = 1.5f;
  private float waveStartTime = 0;
  private EnemySpawnSystem spawnSystem;
  private Timekeeper timekeeper;

  void Start() {
    spawnSystem = GetComponent<EnemySpawnSystem>();
    timekeeper  = Timekeeper.getInstance();
  }

  void Update() {
    if (timekeeper.getTime() > nextSpawnTime) {
      waveStartTime = nextSpawnTime + 0.5f;
      nextSpawnTime = 99999999999999;
      Debug.Log("started spawn");
      spawnSystem.Spawn(wave++);
      Debug.Log("finished spawn");
    } else if ((NumEnemies() > 0) || (timekeeper.getTime() < waveStartTime)) {
      Debug.Log(NumEnemies());
      return;
    } else {
      nextSpawnTime = Mathf.Min(nextSpawnTime,
                                timekeeper.getTime() + secondsBetweenWaves);
      Debug.Log("scheduled next spawn for " + nextSpawnTime);
    }
  }

  int NumEnemies() {
    int num = 0;

    Debug.Log(spawnSystem.currentEnemies.Count + " enemies present");

    foreach (GameObject enemy in spawnSystem.currentEnemies) {
      if (enemy.GetComponent<MemoryComponent>().IsActive()) {
        num++;
      }
    }
    return num;
  }
}
