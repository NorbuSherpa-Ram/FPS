using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth=100;
    [HideInInspector ]
    public int currentHealth;

    public Slider slider;
    private void Start()
    {
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth );
    }
    public void  SetMaxHealth( int maxHp)
    {
        slider.value = maxHp ;
    }
    public void SetCurrentHealth(int currentHp)
    {
        slider.value = currentHp ;
    }
    public void TakeDamage(int damage)
    {
        currentHealth  -= damage;
        SetCurrentHealth(currentHealth);
        if(currentHealth  <=0)
        {
            Destroy(this.gameObject ,.5f);
        }
    }

    
}
