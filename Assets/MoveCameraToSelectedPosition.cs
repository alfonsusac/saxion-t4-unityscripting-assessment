using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraToSelectedPosition : MonoBehaviour
{

    public Transform newCamTransform;
    //public Vector3 newCamPos;
    //public Quaternion newCamRot;

    public float CamMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetCameraPlacement(Transform t)
    {
        newCamTransform = t;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(
            a: transform.rotation,
            b: newCamTransform.rotation,
            t: CamMoveSpeed * Time.deltaTime * Time.timeScale);

        transform.position = Vector3.Slerp(
            a: transform.position,
            b: newCamTransform.position,
            t: CamMoveSpeed * Time.deltaTime * Time.timeScale);

    }
}
