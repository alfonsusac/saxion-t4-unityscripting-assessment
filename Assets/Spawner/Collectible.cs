using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{

    [Header("Properties")]
    public AudioClip collectSound;

    [Header("debug")]
    public bool collected;

   // PRIVATE
    AudioSource sound;
    GameObject model;

   // PUBLIC INTERFACES
    public bool IsCollected
    {
        get { return collected; }
    }
    public void Spawn()
    {
        collected = false;
        model.SetActive(true);
    }


    public void Start()
    {
        sound = GetComponent<AudioSource>();
        if (sound == null) Debug.LogError("Sound Source is not foudn!!");
        model = transform.Find("Model").gameObject;
        if (model == null) Debug.LogError("Spawner Item Must Have a Model!!");
    }

    public void Collect(PlayerClass p)
    {
        if (!IsCollected)
        {
            if(p!=null) OnCollectByPlayer(p);
            collected = true;
            if(sound != null)
                sound.PlayOneShot(collectSound);
            model.SetActive(false);
        }
    }

    protected abstract void OnCollectByPlayer(PlayerClass p);




}
