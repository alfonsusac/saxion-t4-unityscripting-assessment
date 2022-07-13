using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    public Camera mainCam;
    Camera thisCam;


    // Start is called before the first frame update
    void Start()
    {
        thisCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        thisCam.fieldOfView = mainCam.fieldOfView;
    }
}
