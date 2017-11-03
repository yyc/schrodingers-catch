using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitilities.Tuples;

public class EnemySpawnSystem : MonoBehaviour {
  public int currentEnemies = 0;
  public GameObject enemyPrefab;

  int wave            = 0;
  int numWaves        = 0;
  float nextSpawnTime = -1.0f;

  // Minimum distance from a portal
  public int minimumSpawnDistance = 10;

  public Sprite[] enemySprites;

  List<int[]>waves = new List<int[]>();

  // Use this for initialization
  void Start() {
    nextSpawnTime = -1.0f;
    string fileData = System.IO.File.ReadAllText("waves.txt");

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

  public void Despawned(GameObject enemy) {
    currentEnemies--;
  }

  // Update is called once per frame
  void Update() {
    if (currentEnemies > 0) {
      return;
    } else if (nextSpawnTime < 0) {
      nextSpawnTime = Timekeeper.getInstance().maxTime;
    } else if (Timekeeper.getInstance().getTime() > nextSpawnTime) {
      // Increment 1 as a mutex to prevent this from running multiple times
      currentEnemies++;
      Spawn(waves[Mathf.Min(waves.Count - 1, wave)]);
      nextSpawnTime = -1.0f;
      wave++;
    }
  }

  void Spawn(int[] enemies) {
    Timekeeper timekeeper = Timekeeper.getInstance();

    int spritesLength = enemySprites.Length;
    int spriteIndex   = Random.Range(0, spritesLength);

    // Spawn for each entangled group
    for (int i = 0; i < enemies.Length; i++) {
      GameObject[] twins = new GameObject[enemies[i]];
      TupleI observed    = new TupleI(0, enemies[i]);

      for (int j = 0; j < enemies[i]; j++) {
        currentEnemies++;

        GameObject newEnemy = Instantiate(enemyPrefab,
                                          Vector3.zero,
                                          Quaternion.identity);

        twins[j]      = newEnemy;
        newEnemy.name = "Enemy-" + wave + "-" + i + "-" + j;

        newEnemy.GetComponent<MemoryComponent>().position =
          randomValidPosition();
        newEnemy.GetComponent<MemoryComponent>().firstActiveTick =
          timekeeper.getTick();

        newEnemy.GetComponent<SpriteRenderer>().sprite =
          enemySprites[spriteIndex];

        EnemyObservingSystem observer =
          newEnemy.GetComponent<EnemyObservingSystem>();
        observer.twins       = twins;
        observer.observed    = observed;
        observer.spawnSystem = this;
      }

      spriteIndex = (spriteIndex + 1) % spritesLength;
    }
    currentEnemies--;
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
