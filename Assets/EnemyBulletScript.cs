using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class EnemyBulletScript : MonoBehaviour
{

    public GameObject deathParticle;
    public float speedMultiplier;

    bool death;
    Rigidbody rb;

    private void Start()
    {
        death = false;
        rb = this.FindComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rb != null)
            rb.velocity = transform.forward * Time.deltaTime * Time.timeScale * 100 * speedMultiplier;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerClass>() == PlayerClass.Instance)
        {
            PlayerClass.Instance.GetHurt(10);
        }
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    
}
