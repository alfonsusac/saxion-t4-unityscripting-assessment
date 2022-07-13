using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class PlayerLook : MonoBehaviour
{
    public Transform head;
    

    private float currHeadRot = 0;
    public float maxLookUpAngle = 89;
    public float maxLookDownAngle = 89;

    float mouseX;
    float mouseY;

// Start is called before the first frame update
    void Start()
    {
        this.FindObject(ref head, "Head");
    }

// Update is called once per frame
    
    void Update()
    {
    //----------------------------------------------
    // Retrieve Input
        mouseX = PauseMenu.GameIsPaused ? 0f : Input.GetAxis("Mouse X");  // Mouse X -> Left Right
        mouseY = PauseMenu.GameIsPaused ? 0f : Input.GetAxis("Mouse Y");  // Mouse Y -> Up Down

    //----------------------------------------------
    // Rotate PLAYER Around Y-Axis
        transform.Rotate(0, mouseX, 0);   // Rotate Player's whole around delta.mouse.x


    //----------------------------------------------
    // Rotate HEAD Around X-Axis
        RotateHead(-mouseY);
        currHeadRot += -mouseY;                                               // Add it first
        if (currHeadRot < maxLookUpAngle && currHeadRot > -maxLookDownAngle)  // If can still move,
            head.Rotate(-mouseY, 0, 0);                                       //    then rotate.
        else                                                                  // If already at limit, 
            currHeadRot -= -mouseY;                                           //    then remove rotation

    }

    public void RotateHead(float y)
    {
        currHeadRot += y;                                               // Add it first
        if (currHeadRot < maxLookUpAngle && currHeadRot > -maxLookDownAngle)  // If can still move,
            head.Rotate(y, 0, 0);                                       //    then rotate.
        else                                                                  // If already at limit, 
            currHeadRot -= y;
    }


}
