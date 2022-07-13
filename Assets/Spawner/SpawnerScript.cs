using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [Header("Properties")]
    public GameObject SpawnerItemPrefab;
    Collectible SpawnerItem;
    public float SpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        if (SpawnerItem == null)
        {
            SpawnSpawnerItem();
            waitingToSpawn = false;
        }
    }

    [Space(10)]
    [Header("Debug Properties")]
    public float timeSincedestroyed;
    public bool waitingToSpawn;
    // Update is called once per frame
    void Update()
    {
        if(SpawnerItem.IsCollected)
        {
            if (!waitingToSpawn)
            {
                waitingToSpawn = true;
                timeSincedestroyed = 0;
            }
            if(waitingToSpawn)
            {
                timeSincedestroyed += Time.deltaTime * Time.timeScale;
            }
            if(timeSincedestroyed > 5)
            {
                SpawnerItem.Spawn();
                waitingToSpawn=false;
                timeSincedestroyed = 0;
            }
        }
    }

    void SpawnSpawnerItem()
    {
        SpawnerItem = Instantiate(SpawnerItemPrefab, transform.position + Vector3.up, Quaternion.identity)
            .GetComponent<Collectible>();
    }
}
