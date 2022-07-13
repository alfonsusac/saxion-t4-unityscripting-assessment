using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAmmo : Collectible
{
    protected override void OnCollectByPlayer(PlayerClass p)
    {
        p.GetComponent<PlayerHandleGun>()?.AddAmmo(3);
    }
}
