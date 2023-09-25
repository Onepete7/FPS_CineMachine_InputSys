using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDB_GameOverDisplay : MonoBehaviour
{
    GameObject gameOverPanel;

    private void OnEnable()
    {
        GDB_UnityEventHealth.onPlayerDeath += DisplayGameOver;
    }

    private void OnDisable()
    {
        GDB_UnityEventHealth.onPlayerDeath -= DisplayGameOver;
    }

    void DisplayGameOver()
    {

        gameOverPanel.gameObject.SetActive(true);
    }
}
