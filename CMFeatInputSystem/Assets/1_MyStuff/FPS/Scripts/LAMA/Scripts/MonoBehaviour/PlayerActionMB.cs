using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]

public class PlayerActionMB : MonoBehaviour
{
    [SerializeField] PlayerGunSelectorMB lamaPlayerGunSelectorMB;

    private void Update()
    {
        if (Mouse.current.leftButton.isPressed && lamaPlayerGunSelectorMB.lamaActiveGunGunConfigSO != null)
        {
            lamaPlayerGunSelectorMB.lamaActiveGunGunConfigSO.LamaShoot();
        }
    }
}
