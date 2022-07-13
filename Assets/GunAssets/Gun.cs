using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    // GEOMETRY
    public Vector3 BarrelTipLocation { get { return barreltip.position; } }
    private Transform barreltip;

    // DAMAGE
    public int damage;

    // BULLETS
    public int CurrentAmmo { get { return currentAmmo; } }
    public int BulletCapacity;
    private int currentAmmo;

    // FIRE RATES
    private float delayInSeconds { get { return 60f / fireRate; } }
    public int fireRate; // in rpm

    // SOUNDS
    public GameObject shootSound;
    public GameObject emptyMagSound;
    public GameObject reloadSound;

    // DELAYS
    float lastShot = 0;
    public void Initialize(Transform gun)
    {
        lastShot = Time.time;
        barreltip = gun.GetChild(0).Find("BarrelTip");
    }


    public bool Shoot()
    {
        float currentTime = Time.time;
        //Debug.Log(currentTime + " " + lastShot + " " +(currentTime - lastShot) + "    " + delayInSeconds + "  " + currentAmmo);
        if ((currentTime - lastShot) > delayInSeconds)
        {
            //Debug.Log(currentAmmo);
            if (currentAmmo > 0)
            {
                lastShot = currentTime;
                currentAmmo -= 1;
                //Debug.Log("Shot!");

                GameObject sound = Instantiate(shootSound, null);
                sound.transform.position = barreltip.position;

                return true;
            }
            else
            {
                //Debug.Log("Empty Mag!");

                GameObject sound = Instantiate(emptyMagSound, null);
                sound.transform.position = barreltip.position;

                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void PlayReloadSound()
    {
        Debug.Log("Playing reload sound");
        Instantiate(reloadSound, barreltip);
    }

    public bool Reload(ref int magazineCount)
    {
        if(magazineCount > 0)
        {
            magazineCount -= 1;
            currentAmmo = BulletCapacity;

            Debug.Log("Ammo Reloaded!");
            return true;
        }
        return false;
    }


}
