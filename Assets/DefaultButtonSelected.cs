using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultButtonSelected : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
