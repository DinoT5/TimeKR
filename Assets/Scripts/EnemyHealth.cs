using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Image healthBar;
    public BattleEnemy BE;

    private void Start()
    {
        
    }
    private void Update()
    {
        healthBar.fillAmount = BE.curHP/BE.baseHP;
    }


}

