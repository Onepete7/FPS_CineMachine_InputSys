using UnityEngine;
using TMPro;

public class MonsterHealthText : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    void Awake()
    {
        SetHealth(0);
    }

    public void SetHealth(int health)
    {
        healthText.text = health.ToString();
    }

    //Temporary
    public void UpdateHealth(Component sender, object data)
    {
        //Par exemple: if(sender is PlayerHealth), if (sender is Health)
        if (data is int)
        {
            int amount = (int)data;
            SetHealth(amount);
            // Debug.Log($"Updating the health by {amount}");
        }

    }

}
