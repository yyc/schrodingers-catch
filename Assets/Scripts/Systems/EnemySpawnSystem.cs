using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;

public class EnemySpawnSystem : MonoBehaviour {
  public GameObject enemyPrefab;
  public TextAsset waveFile;

  int wave            = 0;
  int numWaves        = 0;
  float nextSpawnTime = -1.0f;

  // Minimum distance from a portal
  public int minimumSpawnDistance = 10;

  public List<GameObject>currentEnemies = new List<GameObject>();

  public RuntimeAnimatorController[] enemyAnimators;

  List<int[]>waves = new List<int[]>();

  // Use this for initialization
  void Start() {
    nextSpawnTime = -1.0f;
    string fileData = waveFile.text;

    string[] rows = fileData.Split("\n"[0]);
    numWaves = rows.Length;

    foreach (string row in rows) {
      string[] values   = row.Split(" "[0]);
      int      waveSize = values.Length;
      int[]    wave     = new int[waveSize];

      for (int i = 0; i < waveSize; i++) {
        int.TryParse(values[i], out wave[i]);
      }
      waves.Add(wave);
    }
  }

  // Update is called once per frame
  void        Update() {}

  public void Spawn(int wave) {
    Spawn(waves[wave]);
  }

  public void Spawn(int[] enemies) {
    Timekeeper timekeeper = Timekeeper.getInstance();

    foreach (GameObject enemy in currentEnemies) {
      Destroy(enemy, 1.0f);
    }
    currentEnemies.Clear();

    int spritesLength = enemyAnimators.Length;
    int spriteIndex   = Random.Range(0, spritesLength);

    // Spawn for each entangled group
    for (int i = 0; i < enemies.Length; i++) {
      GameObject[] twins = new GameObject[enemies[i]];
      TupleI observed    = new TupleI(0, enemies[i]);

      for (int j = 0; j < enemies[i]; j++) {
        GameObject newEnemy = Instantiate(enemyPrefab,
                                          Vector3.zero,
                                          Quaternion.identity);

        twins[j]      = newEnemy;
        newEnemy.name = "Enemy-" + wave + "-" + i + "-" + j;

        currentEnemies.Add(newEnemy);

        newEnemy.GetComponent<MemoryComponent>().position =
          randomValidPosition();
        newEnemy.GetComponent<MemoryComponent>().firstActiveTick =
          timekeeper.getTick();

        newEnemy.GetComponent<AnimatorChildComponent>().animator.
        runtimeAnimatorController = enemyAnimators[spriteIndex];

        EnemyObservingSystem observer =
          newEnemy.GetComponent<EnemyObservingSystem>();
        observer.twins           = twins;
        observer.observed        = observed;
        observer.spawnSystem     = this;
        observer.chargeComponent = GetComponent<ChargeComponent>();
      }

      spriteIndex = (spriteIndex + 1) % spritesLength;
    }
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
