using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HurtScreen : MonoBehaviour
{
    public PlayerClass playerClass;
    Image bloodOverlay;

    // Start is called before the first frame update
    void Start()
    {
        playerClass = PlayerClass.Instance;
        bloodOverlay = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        bloodOverlay.color = new Color(bloodOverlay.color.r, bloodOverlay.color.g, bloodOverlay.color.b, (playerClass.MaxHealth - playerClass.Health) / playerClass.MaxHealth);
    }
}
