using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckMovementTutorial : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tutorialText;
    enum TutorialType
    {
        WASD,
        JUMP,
        SHIFT,
        SHOOT,
        RIGHTCLICK,
        RELOAD,
        ESCAPE
    }
    [SerializeField]
    TutorialType tutorialType;

    bool[ ] check = {false, false, false, false,false, false, false};


    // Start is called before the first frame update
    void Start()
    {
        tutorialText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!check[0] && tutorialType == TutorialType.WASD)
        {
            if(Input.GetAxis("Horizontal") != 0|| Input.GetAxis("Vertical") != 0)
            {
                check[0] = true;
                tutorialText.text = tutorialText.text + "\n DONE!";
            }
        }


        if (!check[1] && tutorialType == TutorialType.JUMP)
        {
            if (Input.GetAxis("Jump") != 0)
            {
                check[1] = true;
                tutorialText.text = tutorialText.text + "DONE!";
            }
        }

        if (!check[2] && tutorialType == TutorialType.SHIFT)
        {
            if (Input.GetAxis("Crouch") != 0)
            {
                check[2] = true;
                tutorialText.text = tutorialText.text + "DONE!";
            }
        }

        if (!check[3] && tutorialType == TutorialType.SHOOT)
        {
            if (Input.GetAxis("Fire1") != 0)
            {
                check[3] = true;
                tutorialText.text = tutorialText.text + "DONE!";
            }
        }
        
        if (!check[4] && tutorialType == TutorialType.RIGHTCLICK)
        {
            if (Input.GetAxis("Fire2") != 0)
            {
                check[4] = true;
                tutorialText.text = tutorialText.text + "DONE!";
            }
        }


    }
}
