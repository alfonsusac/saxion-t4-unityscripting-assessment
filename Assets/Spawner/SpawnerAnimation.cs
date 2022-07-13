using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,Time.deltaTime * Time.timeScale * 50,0);
        //Debug.Log(Mathf.Sin(Time.time));
        transform.localPosition = new Vector3(0, Mathf.Sin(Time.time * 3) / 3 , 0);
    }
}
