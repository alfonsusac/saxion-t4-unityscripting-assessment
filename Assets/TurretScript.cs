using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class TurretScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    public GameObject _bullet;

    // Start is called before the first frame update
    void Start()
    {
        _player = PlayerClass.Instance.gameObject;
    }


    public float shootDelay;
    public float shootCurr;
    // Update is called once per frame
    void Update()
    {
        if(_player != null)
        {
            Vector3 diff = _player.transform.position - transform.position;

            if (CheckLineofSights(_player.transform))
            {
                transform.rotation = Quaternion.RotateTowards(
                    from: transform.rotation,
                    to: Quaternion.LookRotation(diff + Vector3.up * 2f),
                    maxDegreesDelta: 1);

                shootCurr += Time.deltaTime * Time.timeScale;
                if(shootCurr > shootDelay)
                {
                    FireBullet();
                    shootCurr = 0;
                }
            }
        }
    }

    void FireBullet()
    {
        Instantiate(_bullet, transform.position + transform.forward * 2, transform.rotation);
    }

    bool CheckLineofSights(Transform obj)
    {
        Vector3 diff = obj.position - transform.position;


        RaycastHit hitInfo;
        Debug.DrawRay(transform.position, diff);
        if (Physics.Raycast(transform.position, diff, out hitInfo, Mathf.Infinity))
        {
            if (hitInfo.transform == PlayerClass.Instance.transform)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    private void OnDestroy()
    {
        
    }
}
