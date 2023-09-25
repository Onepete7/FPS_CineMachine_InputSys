using System.Collections;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public float debuffCooldown = 1f;


    [Header("Events")]
    public GameEvent onPlayerHealthChanged;
    private int currentHealth;
    private WaitForSeconds waitForDebuff;

    private void Awake()
    {
        currentHealth = startingHealth;
        waitForDebuff = new WaitForSeconds(debuffCooldown);

        StartCoroutine(TakeDamage(1));

        //Apply periodic damage until health == 0
    }

    IEnumerator TakeDamage(int amount)
    {
        while (currentHealth > 0)
        {
            currentHealth -= amount;

            onPlayerHealthChanged.Raise(this, currentHealth);

            yield return waitForDebuff;
        }
    }

}
