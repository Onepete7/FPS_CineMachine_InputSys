using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GDB_UnityEventHealth : MonoBehaviour
{
    public float hp = 100;
    public static event Action onPlayerDeath;
    public static event Action<float> onPlayerHurt;

    public void RemoveHealth(float amount)
    {
        hp -= amount;

        onPlayerHurt?.Invoke(hp);

        if (hp < 0)
        {
            onPlayerDeath?.Invoke();
        }
    }

    void PlayerDeathFunction()
    {
        //Kill the player
    }
}
