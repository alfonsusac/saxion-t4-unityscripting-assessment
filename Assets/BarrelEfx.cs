using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelEfx : MonoBehaviour
{

    List<ParticleSystem> muzzleFlashList;
    Light flash;

    private void Start()
    {
        muzzleFlashList = new List<ParticleSystem>();

        for (int i = 0; i < transform.childCount; i++)
        {
            muzzleFlashList.Add(transform.GetChild(i).GetComponent<ParticleSystem>());
        }
    }

    public void FireAnime()
    {
        foreach(ParticleSystem p in muzzleFlashList)
        {
            p.Play();
        }
    }
}
