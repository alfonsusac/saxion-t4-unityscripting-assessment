using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSound : MonoBehaviour
{
    public AudioSource a;

    // Update is called once per frame
    void Update()
    {
        a = GetComponent<AudioSource>();
        if(a.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
