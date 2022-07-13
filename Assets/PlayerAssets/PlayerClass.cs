using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerClass : MonoBehaviour
{
    public static PlayerClass Instance;
    [Header("Player Attributes")]
    [Space(10)]
    // UI Component
    [SerializeField] Slider healthBarSlider;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] AudioClip Sound_GetShot;
    AudioSource playerAudio;

    // Player Status
    [SerializeField]
    private static float _health;
    internal bool _isAlive;
    public Entity _entity;
    public Entity Entity { get { return _entity; } }
    public float Health { get { return _health; } }
    public float MaxHealth { get { return _entity.maxHealth; } }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("2 Different Player Huh?");
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        if (healthBarSlider == null) Debug.LogError("Health Bar Slider Not Found!");
        if (healthText == null) Debug.LogError("Health Text Not Found!");
        healthBarSlider.maxValue = _entity.maxHealth;
        
        _isAlive = true;
        _health = _entity.maxHealth;
    }

    internal void GetHurt(float damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            Death();
        }
        GetComponent<PlayerHandleGun>().gunAndHandModel.Rotate(-7, 7, 0);
        playerAudio.PlayOneShot(Sound_GetShot);
    }

    internal void Death()
    {
        PauseMenu.RequestGameOverScreen();
    }

    internal void Heal(float amount)
    {
        _health += amount;
    }

    private float amountToHeal = 0;
    internal void HealOverTime(float amount)
    {
        amountToHeal += amount;
    }

    void Update()
    {
        healthBarSlider.value = _health;
        healthText.text = "" + Mathf.Ceil(_health);

        if(amountToHeal > 0)
        {
            if(_health <= MaxHealth)
            {
                _health++;
            }
            amountToHeal--;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        collision.GetComponent<Collectible>()?.Collect(this);
    }
}
