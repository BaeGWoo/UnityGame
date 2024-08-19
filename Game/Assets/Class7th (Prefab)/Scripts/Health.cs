using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float initHealth;
    private float health = 100;
    [SerializeField] Slider healthSlider;

    [SerializeField] int[] HealthState;
    [SerializeField] Image sliderColor;
    [SerializeField] Transform Area;
    private void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
        Area = healthSlider.transform.Find("Fill Area");
        sliderColor=Area.GetComponentInChildren<Image>();
    }

    void Start()
    {
       
        initHealth = health;
        healthSlider.value = health / initHealth;
    }

    public void OnState()
    {
        if (health <= HealthState[0])
        {
            sliderColor.color = Color.red;
        }

        else if (health <= HealthState[1])
        {
            sliderColor.color = Color.yellow;
        }

        else
        {
            sliderColor.color = Color.green;
        }

    }

    public void OnDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health / initHealth;

        OnState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnDamage(10);
        }

      
       
    }


}
