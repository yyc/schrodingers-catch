using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;
using UnityEngine.UI;

public class WaveControllerSystem : MonoBehaviour {
  public float secondsBetweenWaves = 3.0f;
  public int wave                  = 0;

  public float nextSpawnTime = 1.5f;
  public float waveStartTime = 0;

  public Text enemyCountText;
  private EnemySpawnSystem spawnSystem;
  private Timekeeper timekeeper;

  void Start() {
    spawnSystem = GetComponent<EnemySpawnSystem>();
    timekeeper  = Timekeeper.getInstance();
  }

  void Update() {
    int enemies = NumEnemies();

    if (enemyCountText != null) {
      enemyCountText.text = enemies + "";
    }

    if (timekeeper.getTime() > nextSpawnTime) {
      waveStartTime = nextSpawnTime + 0.5f;
      nextSpawnTime = 99999999999999;
      spawnSystem.Spawn(wave++);
    } else if ((enemies > 0) || (timekeeper.getTime() < waveStartTime)) {
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
