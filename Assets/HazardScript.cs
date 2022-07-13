using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour
{

    public float damage = 5;
    public float delay = 1;
    public float curr;

    private void Start()
    {
        curr = 900;
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(Time.timeScale);
        PlayerClass p = collision.gameObject.GetComponent<PlayerClass>();
        if(p != null)
        {
            curr += Time.deltaTime * Time.timeScale;
            if(curr > delay)
            {
                curr = 0;
                p.GetHurt(damage);
            }
        }
    }

    
}
