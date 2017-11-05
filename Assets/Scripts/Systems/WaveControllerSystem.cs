using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;

public class WaveControllerSystem : MonoBehaviour {
  public float secondsBetweenWaves = 3.0f;
  public int wave                  = 0;

  public float nextSpawnTime = 1.5f;
  public float waveStartTime = 0;
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
      spawnSystem.Spawn(wave++);
    } else if ((NumEnemies() > 0) || (timekeeper.getTime() < waveStartTime)) {
      Debug.Log(NumEnemies());
      return;
    } else {
      nextSpawnTime = Mathf.Min(nextSpawnTime,
                                timekeeper.getTime() + secondsBetweenWaves);
    }
  }

  int NumEnemies() {
    int num = 0;

    foreach (GameObject enemy in spawnSystem.currentEnemies) {
      if (enemy.GetComponent<MemoryComponent>().IsActive()) {
        num++;
      }
    }
    return num;
  }
}
