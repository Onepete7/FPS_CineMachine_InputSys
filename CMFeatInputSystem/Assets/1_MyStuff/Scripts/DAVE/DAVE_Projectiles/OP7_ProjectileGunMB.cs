using UnityEngine;

public class OP7_ProjectileGunMB : MonoBehaviour
{
    //bullet
    GameObject opBullet;

    //bullet force
    float opShootForce, opUpwardForce;

    //Gun stats
    float opTimeBetweenShooting, opSpread, opReloadTime, opTimeBetweenShots;
    int opMagazineSize, opBulletsPerTap;
    bool opDoesAllowButtonHold;
    int opBulletsLeft, opBulletsShot;

    //bools
    bool opIsShooting, opIsReadyToShoot, opIsReloading;

    //Reference
    Camera opFPSCam;
    Transform opAttackPoint;

    //bug fixing
    bool opDoesAllowInvoke = true;

    private void Awake()
    {
        opBulletsLeft = opMagazineSize;
        opIsReadyToShoot = true;
    }

    private void Update()
    {
        OPMyInput();
    }

    private void OPMyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        if (opDoesAllowButtonHold)
        {
            opIsShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            opIsShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        //Shooting
        if (opIsReadyToShoot && opIsShooting && !opIsReloading && opBulletsLeft > 0)
        {
            //Set bullets shot to 0
            opBulletsShot = 0;
            OPShoot();
        }
    }

    void OPShoot()
    {
        opIsReadyToShoot = false;

        //Find the exact hit position using a raycast
        Ray opRay = opFPSCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit opHit;

        //check if ray hits something
        Vector3 opTargetPoint;
        if (Physics.Raycast(opRay, out opHit)) //Raycast is a bool, takes a ray, out something hit
        {
            opTargetPoint = opHit.point;
        }
        else
        {
            opTargetPoint = opRay.GetPoint(75.0f); //Just a point far away from the player
        }

        //Calculate 4:52 SHOOTING with BULLETS + CUSTOM PROJECTILES

        opBulletsLeft--;
        opBulletsShot++;
    }


}

