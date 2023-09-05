using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OP7_PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public Animator anim;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            //Deadth
            //PlayerCantMove
            //GameOverAnimation

            anim.SetBool("isDead", true);
        }
    }

    public void HealDamage(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }


}
