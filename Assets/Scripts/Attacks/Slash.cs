using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : BaseAttack
{
    // Start is called before the first frame update
    public Slash()
    {
        attackName = "Slash";
        attackDes = "a downwards slash";
        attackDamage = 15f;
        attackCost = 0;
    }
}
