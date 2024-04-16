using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleDantes : BaseClass
{
    public int stamina;
    public int intellect;
    public int dexterity;
    public int agility;

    public List<BaseAttack> MagicAttacks = new List<BaseAttack>();

}
