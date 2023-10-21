using UnityEngine;

[CreateAssetMenu(fileName = "OP7_ProjectileGunScriptableOBJ", menuName = "CMFeatInputSystem/OP7_ProjectileGunScriptableOBJ", order = 0)]
public class OP7_ProjectileGunScriptableOBJ : ScriptableObject
{
    GameObject opBullet;
    float opShootForce, opUpwardForce;
    float opTimeBetweenShooting, opSpread, opReloadTime, opTimeBetweenShots;
    int opMagazineSize, opBulletsPerTap;
    bool opDoesAllowButtonHold;

    int opBulletsLeft, opBulletsShot;

    bool opIsShooting, opIsReadyToShoot, opIsReloading;
}