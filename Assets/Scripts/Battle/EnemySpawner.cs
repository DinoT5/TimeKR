using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{   

    [SerializeField] private List<BattleEnemy> _battleEnemies = new List<BattleEnemy>();
    [SerializeField] private List<Transform> _enemyPositions = new List<Transform>();
    [SerializeField] private int _enemiesToSpawn = 3;



    private void Awake()
    {
        _enemiesToSpawn = PlayerPrefs.GetInt("EnemiesSpawned");
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < _enemiesToSpawn; i++)
        {
            int RandomNumber = Random.Range(0, _battleEnemies.Count);
            Instantiate(_battleEnemies[RandomNumber], _enemyPositions[i].position, _enemyPositions[i].rotation);
        }
    }



}
