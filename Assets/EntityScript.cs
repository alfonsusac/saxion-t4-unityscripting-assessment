using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    public Entity _entity;
    public Entity Entity { get { return _entity; } }

    internal bool _isAlive;
    private float _health;
    public float Health { get { return _health; } }

    // Start is called before the first frame update
    void Start()
    {
        _isAlive = true;
        _health = _entity.maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if (!_isAlive)
        {
            Destroy(gameObject);
        }
    }

    public void Attack(EntityScript e, float dmg)
    {
        e.ReceiveDamage(dmg);
    }


    public void ReceiveDamage(float dmg)
    {
        Debug.Log("Dmg Received to " + gameObject.name + " (" + dmg + ") | Current Health: " + _health);
        _health -= dmg;
        if (_health <= 0)
        {
            _isAlive = false;
            Die();
        }
    }
    public void Die(string Reason = "")
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            PlayerScore.AddScore(100);
        }
        Debug.Log("Death! (by:"+Reason+")");
        _isAlive = false;
    }

    public void SetHealth(float h)
    {
        _health = h;
    }



}
