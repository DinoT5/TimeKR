using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject EnemyPrefab;
    // Start is called before the first frame update
    public void SelectEnemy()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().Input2(EnemyPrefab);
    }

}