using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : Collectible
{
    protected override void OnCollectByPlayer(PlayerClass p)
    {
        p.HealOverTime(10);
    }
}
