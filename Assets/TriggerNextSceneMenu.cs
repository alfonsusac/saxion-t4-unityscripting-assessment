using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNextSceneMenu : MonoBehaviour
{

    PlayerClass p;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerClass>() == PlayerClass.Instance)
        {
        Debug.Log("Aaa");
            PauseMenu.RequestNextLevelScreen();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
