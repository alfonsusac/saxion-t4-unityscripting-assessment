using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageIndicatorScript : MonoBehaviour
{
    Transform Player;

    [SerializeField]
    private TextMeshProUGUI Text;
    private float maxLifespan = 1; // seconds
    private float lifespan = 0;

    [SerializeField]
    private float lifepercentage;

    public string text;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("MainCamera").transform;
    }
    bool calledOnce = false;


    // Update is called once per frame
    void Update()
    {
        Text.text = text;
        lifespan += Time.deltaTime * Time.timeScale;
        lifepercentage = lifespan / maxLifespan;
        transform.LookAt(Player);

        if (!calledOnce)
        {
            transform.position = Player.position - transform.forward * 3;
        } 

        transform.Translate(Vector3.up * 0.01f,Space.World);
        Color c = Color.white;
        c.a = 1 - 1 * lifepercentage;
        //Debug.Log(c.a);
        Text.color = c;

        if (lifespan > maxLifespan) Destroy(gameObject);
    }

}
