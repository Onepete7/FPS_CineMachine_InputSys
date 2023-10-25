// using TMPro;
// using UnityEngine;

// public class OP7_ProjectileGunMB : MonoBehaviour
// {
//     //bullet
//     [SerializeField] GameObject opBulletGO;

//     //bullet force
//     float opShootForce = 300.0f, opUpwardForce = 0.0f;

//     //Gun stats
//     float opTimeBetweenShooting = 0.1f, opSpread = 3.0f, opReloadTime = 1.5f, opTimeBetweenShots = 0.0f;
//     int opMagazineSize = 100, opBulletsPerTap = 1;
//     bool opDoesAllowButtonHold = true;


//     int opBulletsLeft, opBulletsShot;

//     //bools
//     bool opIsShooting, opIsReadyToShoot, opIsReloading;

//     //Reference
//     [SerializeField] Camera opFPSCam;
//     [SerializeField] Transform opAttackPointTransform;

//     //Graphics
//     [SerializeField] GameObject opMuzzleFlashGO; //The flash at the point of the gun
//     [SerializeField] TextMeshProUGUI opAmmunitionDisplayTMPUGUI;

//     //bug fixing LOL
//     bool opDoesAllowShootingInvoke = true;

//     private void Awake()
//     {
//         opBulletsLeft = opMagazineSize;
//         opIsReadyToShoot = true;
//     }

//     private void Update()
//     {
//         OPMyInput();

//         //Set ammo display, if it exists LOL
//         if (opAmmunitionDisplayTMPUGUI != null)
//         {
//             opAmmunitionDisplayTMPUGUI.SetText(opBulletsLeft / opBulletsPerTap + " / " + opMagazineSize / opBulletsPerTap);
//         }
//     }

//     private void OPMyInput()
//     {
//         //Check if allowed to hold down button and take corresponding input
//         if (opDoesAllowButtonHold)
//         {
//             opIsShooting = Input.GetKey(KeyCode.Mouse0);
//         }
//         else
//         {
//             opIsShooting = Input.GetKeyDown(KeyCode.Mouse0);
//         }

//         //Reloading
//         if (Input.GetKeyDown(KeyCode.R) && opBulletsLeft < opMagazineSize && !opIsReloading)
//         {
//             Reload();
//         }
//         //Reload automatically when trying to shoot without ammo
//         if (opIsReadyToShoot && opIsShooting && !opIsReloading && opBulletsLeft <= 0)
//         {
//             Reload();
//         };

//         //Shooting
//         if (opIsReadyToShoot && opIsShooting && !opIsReloading && opBulletsLeft > 0)
//         {
//             //Set bullets shot to 0
//             opBulletsShot = 0;
//             OPShoot();
//         }
//     }

//     void OPShoot()
//     {
//         opIsReadyToShoot = false;

//         //Find the exact hit position using a raycast
//         Ray opRay = opFPSCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
//         RaycastHit opHit;

//         //check if ray hits something
//         Vector3 opTargetPoint;
//         if (Physics.Raycast(opRay, out opHit)) //Raycast is a bool, takes a ray, out something hit
//         {
//             opTargetPoint = opHit.point;
//         }
//         else
//         {
//             opTargetPoint = opRay.GetPoint(75.0f); //Just a point far away from the player
//         }

//         //Calculate direction from attackPoint to targetPoint
//         Vector3 opDirectionWithoutSpread = opTargetPoint - opAttackPointTransform.position; //target : place of the target; attackpoint : point of the gun;

//         //Calculate spread
//         float x = Random.Range(-opSpread, opSpread);
//         float y = Random.Range(-opSpread, opSpread);

//         //Calculate new direction with spread
//         Vector3 opDirectionWithSpread = opDirectionWithoutSpread + new Vector3(x, y, 0); //Just add spread up/right 

//         //Instantiate bullet/projectile
//         GameObject opCurrentBullet = Instantiate(opBulletGO, opAttackPointTransform.position, Quaternion.identity);
//         //Rotate bullet ?? to shoot direction
//         opCurrentBullet.transform.forward = opDirectionWithSpread.normalized;

//         //Add forces to bullet
//         opCurrentBullet.GetComponent<Rigidbody>().AddForce(opDirectionWithSpread.normalized * opShootForce, ForceMode.Impulse);
//         opCurrentBullet.GetComponent<Rigidbody>().AddForce(opFPSCam.transform.up * opUpwardForce, ForceMode.Impulse); //Bouncing grenades

//         //Instantiate muzzle flash, if you have one
//         if (opMuzzleFlashGO != null)
//         {
//             Instantiate(opMuzzleFlashGO, opAttackPointTransform.position, Quaternion.identity);
//         }

//         opBulletsLeft--;
//         opBulletsShot++;

//         //Invoke resetShot function (if not already invoked)
//         if (opDoesAllowShootingInvoke)
//         {
//             Invoke("OPResetShot", opTimeBetweenShooting); //Invoke is the bad way to do CoRoutine (uses strings, bleh)
//             opDoesAllowShootingInvoke = false;
//         }

//         //If more than one bulletsPerTap make sure to repeat shoot function
//         if (opBulletsShot < opBulletsPerTap && opBulletsLeft > 0)
//         {
//             Invoke("OPShoot", opTimeBetweenShots);
//         }
//     }

//     void OPResetShot()
//     {
//         //Allow shooting and invoking again
//         opIsReadyToShoot = true;
//         opDoesAllowShootingInvoke = true;

//     }

//     void Reload()
//     {
//         opIsReloading = true;
//         Invoke("OPReloadFinished", opReloadTime);
//     }

//     private void ReloadFinished()
//     {
//         opBulletsLeft = opMagazineSize;
//         opIsReloading = false;
//     }




// }

