using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleEnemy : BaseClass
{
    public enum Type
    {
        GRASS,
        FIRE,
        WATER,
        ELECTRIC
    }

    public enum Rarity
    {
        COMMON,
        UNCOMMON,
        RARE,
        SUPERARE
    }
    public Type EnemyType;
    public Rarity rarity;
}
