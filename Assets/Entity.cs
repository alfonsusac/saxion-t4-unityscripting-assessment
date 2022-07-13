using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "Entity")]
public class Entity : ScriptableObject
{
    public float maxHealth;

    private float _speed;
    public float Speed { get { return _speed; } }

}
