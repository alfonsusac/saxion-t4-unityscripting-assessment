using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utility;

public class PlayerHandleGun : MonoBehaviour
{
//--------------------------------------------------------------
// SUMMARY
// Handles:
//  - Player WASD
//  - Jumping


//----------------------------------------
// INSPECTOR Attributes
    [Header("Object References")]
    public Transform head;
    public Transform gunAndHandModel;
    public int MagazineCount;
    public GameObject bulletHole;
    public GameObject bulletHoleFlesh;
    public GameObject bloodStream;
    public GameObject damageIndicator;
    public Animator gunAnimation;
    public Animator camAnimation;
    [Space(10)]
    [Header("Attributes")]
    public float reloadAnimationTime = 1.167f;
    public Transform barrelEfx;

    //----------------------------------------
    // PRIVATE
    bool isGrounded;
    bool isAiming;
    bool leftclick;
    bool rightclick;
    bool reloadclick;
    Rigidbody rb;
    Quaternion initialGunRotation;
    private Camera mainCamera;
    public PlayerLook playerLook;

    public TextMeshProUGUI currentAmmoText;
    public TextMeshProUGUI magazineCountText;

//----------------------------------------
// PUBLIC FIELDS
    public Gun gun;
    public PlayerPhysicalMovement playerphys;
    public PlayerClass playerScript;
    private Entity player;

    bool isReloading;
    public float reloadTimeMult = 1;

//----------------------------------------------------
// METHODS
//----------------------------------------------------
    void Start()
    {
        // GUN
        if (gun == null) Debug.LogError("Gun Scriptable Object Not Found!");
        if (gunAndHandModel == null) Debug.LogError("gunAndHandModel Object Not Found!");
        if (gunAnimation == null) Debug.LogError("gunAnimation Object Not Found!");
        gun.Reload(ref MagazineCount);
        gun.Initialize(gunAndHandModel);
        initialGunRotation = gunAndHandModel.rotation;

        // HEAD
        this.FindObject(ref head, "Head");
        playerphys = this.FindComponent<PlayerPhysicalMovement>();
        playerScript = this.FindComponent<PlayerClass>();
        player = playerScript.Entity;

        // CAMERA
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerLook = GetComponent<PlayerLook>();
    }
    //----------------------------------------------------

    bool leftclickonce = false;
    void Update()
    {
    //----------------------------------------------
    // Retrieving Input
        leftclick = Input.GetAxis("Fire1") > 0.5f && !PauseMenu.GameIsPaused ? true : false;
        rightclick = Input.GetAxis("Fire2") > 0.5f && !PauseMenu.GameIsPaused ? true : false;
        reloadclick = Input.GetAxis("Reload") > 0.5f && !PauseMenu.GameIsPaused ? true : false;
        isGrounded = playerphys.IsGrounded;

    //----------------------------------------------
    // Applying Left Click

        if (reloadclick)
        {
            Debug.Log("Test");
            ReloadGun();
        }

        if (leftclick)
        {
            if (gun.CurrentAmmo != 0 && !isReloading) 
                ShootGun();
            else if (!isReloading)
            {
                if(MagazineCount > 0)
                {
                    ReloadGun();
                    gunAnimation.SetBool("isADS", false);
                }
                else
                {
                    if (!leftclickonce)
                    {
                        ShootGun();
                        leftclickonce = true;
                    }
                }
            }
        }
        else
        {
            leftclickonce = false;
        }

        if (rightclick && !isReloading)
        {
            AimDownSight();
        }
        if(!rightclick || isReloading)
        {
            gunAnimation.SetBool("isADS", false);
            camAnimation.SetBool("isADS", false);
        }

        //----------------------------------------------
        // Resetting Position

        if (gunAndHandModel != null)
        {
            gunAndHandModel.rotation = Quaternion.Lerp(
                a: gunAndHandModel.rotation,
                b: playerphys.RotationXYZ,
                t: Time.deltaTime * 10 * Time.timeScale);
        }
        gunAndHandModel.Rotate(PauseMenu.GameIsPaused ? 0f : -Input.GetAxis("Mouse Y"), PauseMenu.GameIsPaused ? 0f : Input.GetAxis("Mouse X"), 0);
        
    //----------------------------------------------
    // Text GUI Update

        currentAmmoText.text = gun.CurrentAmmo + "";
        magazineCountText.text = MagazineCount + "";

    }
    //----------------------------------------------------

    void ShootGun()
    {
        bool shot = gun.Shoot();
        if (shot)
        {
            // Apply Animation
            barrelEfx.GetComponent<BarrelEfx>().FireAnime();

            // Register Hit
            Debug.DrawRay(gun.BarrelTipLocation, gunAndHandModel.forward * 100f, Color.red, 100f, true);
            RaycastHit hitInfo;
            if (Physics.Raycast(gun.BarrelTipLocation, gunAndHandModel.forward,out hitInfo))
            {



                EntityScript e = hitInfo.transform.gameObject.GetComponent<EntityScript>();
                if (e != null)
                {
                    // BLOOD DECAL
                    Attack(e, gun.damage);
                    GameObject b = Instantiate(
                        original: bulletHoleFlesh,
                        position: hitInfo.point + hitInfo.normal * 0.01f,            // at point of impact
                        rotation: Quaternion.LookRotation(hitInfo.normal) // normal to surface level, converted to quaternion
                        );
                    b.transform.parent = hitInfo.transform;

                    // BLOOD EFFEECT
                    b = Instantiate(
                        original: bloodStream   ,
                        position: hitInfo.point + hitInfo.normal * 0.01f,            // at point of impact
                        rotation: Quaternion.LookRotation(hitInfo.normal) // normal to surface level, converted to quaternion
                        );
                    b.transform.parent = hitInfo.transform;

                    b = Instantiate(
                        original: damageIndicator,
                        position: hitInfo.point,           // at point of impact
                        rotation: Quaternion.identity
                        );
                    b.GetComponent<DamageIndicatorScript>().text = gun.damage + "";
                }
                else
                {
                    GameObject b = Instantiate(
                        original: bulletHole,
                        position: hitInfo.point + hitInfo.normal * 0.01f,            // at point of impact
                        rotation: Quaternion.LookRotation(hitInfo.normal) // normal to surface level, converted to quaternion
                        );
                    b.transform.parent = hitInfo.transform;

                }

            }



            // Apply Recoil
            if (gunAndHandModel)
            {
                if (!rightclick)
                {
                    gunAndHandModel.Rotate(Random.Range(-8, -2), Random.Range(-4, 4), 0);
                    //head.Rotate(Random.Range(-1, -1), 0, 0);
                    playerLook.RotateHead(Random.Range(-1,0));
                }
                if (rightclick)
                {
                    gunAndHandModel.Rotate(Random.Range(-2, -1), Random.Range(-1, 1), 0);
                    playerLook.RotateHead(Random.Range(-0.5f, 0));
                    //head.Rotate(Random.Range(-0.5f, -0.5f), 0, 0);
                }
            }

        }
    }


    void ReloadGun()
    {
        if (!isReloading)
        {
            isReloading = true;
            gunAnimation.SetFloat("reloadSpeedMult", reloadTimeMult);
            gunAnimation.SetTrigger("Reload");
        }
    }
    public void OnFinishReload()
    {
        isReloading = false;
        gun.Reload(ref MagazineCount);
    }

    public void Attack(EntityScript e, float dmg)
    {
        e.ReceiveDamage(dmg);
    }

    void AimDownSight()
    {
        gunAnimation.SetBool("isADS", true);
        gunAnimation.SetBool("isRunning", false);

        camAnimation.SetBool("isADS", true);
        camAnimation.SetBool("isRunning", false);
    }
    public void AddAmmo(int m)
    {
        if (m < 1) return;
        MagazineCount += m;
    }

}
