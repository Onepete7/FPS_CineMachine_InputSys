using UnityEngine;

[CreateAssetMenu(fileName = "Shooting Config", menuName = "Guns/Shooting Config", order = 2)]

public class ShootingConfigSO : ScriptableObject
{
    public LayerMask lamaShootingHitMaskLayerMask;
    public Vector3 lamaShootingSpreadVector3 = new Vector3(0.1f, 0.1f, 0.1f);
    public float lamaShootingFireRateFloat = 0.25f;
}
