using System.Collections;
using UnityEngine;
using UnityEngine.Pool;


[CreateAssetMenu(fileName = "Gun Config", menuName = "Guns/Gun Config", order = 0)]

public class GunConfigSO : ScriptableObject
{
    // public ImpactType impactType; //UNLOCK WITH SURFACE MANAGER GITHUB
    public LamaGunTypeEnum lamaTypeLamaGunTypeEnum;
    public string lamaGunNameString;
    public GameObject lamaGunModelPrefabGO;

    public Vector3 lamaGunSpawnPointVector3;
    public Vector3 lamaGunSpawnRotationVector3;

    public ShootingConfigSO shootingConfigSO;
    public BulletTrailConfigSO bulletTrailConfigSO;

    MonoBehaviour lamaActiveMonoBehaviour;
    GameObject lamaModelGO;
    float lamaLastShootTimeFloat;
    ParticleSystem lamaShootParticleSystem;
    ObjectPool<TrailRenderer> lamaBulletTrailObjectPoolTrailRenderer;


    public void LamaBulletSpawn(Transform lamaParentTransform, MonoBehaviour lamaActiveMonoBehaviour)
    {
        this.lamaActiveMonoBehaviour = lamaActiveMonoBehaviour;
        lamaLastShootTimeFloat = 0; // in editor this will not be properly reset, in build it's cool
        lamaBulletTrailObjectPoolTrailRenderer = new ObjectPool<TrailRenderer>(LamaCreateTrailRenderer);
        lamaModelGO = Instantiate(lamaGunModelPrefabGO);
        lamaModelGO.transform.SetParent(lamaParentTransform, false);
        lamaModelGO.transform.localPosition = lamaGunSpawnPointVector3;
        lamaModelGO.transform.localRotation = Quaternion.Euler(lamaGunSpawnRotationVector3);

        lamaShootParticleSystem = lamaModelGO.GetComponentInChildren<ParticleSystem>();
    }


    public void LamaShoot()
    {
        if (Time.time > shootingConfigSO.lamaShootingFireRateFloat + lamaLastShootTimeFloat)
        {
            lamaLastShootTimeFloat = Time.time;
            lamaShootParticleSystem.Play();
            Vector3 lamaShootDirectionVector3 = lamaShootParticleSystem.transform.forward
                + new Vector3(
                    Random.Range(
                        -shootingConfigSO.lamaShootingSpreadVector3.x,
                        shootingConfigSO.lamaShootingSpreadVector3.x
                    ),
                    Random.Range(
                        -shootingConfigSO.lamaShootingSpreadVector3.y,
                        shootingConfigSO.lamaShootingSpreadVector3.y
                    ),
                    Random.Range(
                        -shootingConfigSO.lamaShootingSpreadVector3.z,
                        shootingConfigSO.lamaShootingSpreadVector3.z
                    )
                );
            lamaShootDirectionVector3.Normalize();

            if (Physics.Raycast(
                lamaShootParticleSystem.transform.position,
                lamaShootDirectionVector3,
                out RaycastHit raycastHit,
                float.MaxValue,
                shootingConfigSO.lamaShootingHitMaskLayerMask
            ))
            {
                lamaActiveMonoBehaviour.StartCoroutine(
                    LamaPlayTrailIEnumerator(
                        lamaShootParticleSystem.transform.position,
                        raycastHit.point,
                        raycastHit
                    )
                );
            }
            else
            {
                lamaActiveMonoBehaviour.StartCoroutine(
                    LamaPlayTrailIEnumerator(
                    lamaShootParticleSystem.transform.position,
                    lamaShootParticleSystem.transform.position + (lamaShootDirectionVector3 * bulletTrailConfigSO.lamaBulletTrailMissDistanceFloat),
                    new RaycastHit()
                )
                );
            }
        }
    }




    private IEnumerator LamaPlayTrailIEnumerator(Vector3 lamaStartPointVector3, Vector3 lamaEndPointVector3, RaycastHit raycastHit)
    {
        TrailRenderer lamaInstanceTrailRenderer = lamaBulletTrailObjectPoolTrailRenderer.Get();
        lamaInstanceTrailRenderer.gameObject.SetActive(true);
        lamaInstanceTrailRenderer.transform.position = lamaStartPointVector3;
        yield return null; //avoid position carry-over from last fram if reused

        lamaInstanceTrailRenderer.emitting = true;

        float lamaDistance = Vector3.Distance(lamaStartPointVector3, lamaEndPointVector3);
        float lamaRemainingDistance = lamaDistance;
        while (lamaRemainingDistance > 0)
        {
            lamaInstanceTrailRenderer.transform.position = Vector3.Lerp(
                lamaStartPointVector3,
                lamaEndPointVector3,
                Mathf.Clamp01(1 - (lamaRemainingDistance / lamaDistance))
            );

            lamaRemainingDistance -= bulletTrailConfigSO.lamaBulletTrailSimulationSpeedFloat * Time.deltaTime;
            yield return null;
        }

        lamaInstanceTrailRenderer.transform.position = lamaEndPointVector3;

        //UNLOCK WITH SURFACE MANAGER GITHUB
        // if (raycastHit.collider != null)
        // {
        //     SurfaceManager.lamaInstanceGO.HandleImpact(
        //         raycastHit.transform.gameObject,
        //         lamaEndPointVector3,
        //         raycastHit.normal,
        //         ImpactType,
        //         0
        //     );
        // }

        yield return new WaitForSeconds(bulletTrailConfigSO.lamaBulletTrailDurationFloat);
        yield return null;
        lamaInstanceTrailRenderer.emitting = false;
        lamaInstanceTrailRenderer.gameObject.SetActive(false);
        lamaBulletTrailObjectPoolTrailRenderer.Release(lamaInstanceTrailRenderer);
    }



    private TrailRenderer LamaCreateTrailRenderer()
    {
        GameObject lamaInstanceGO = new GameObject("Bullet Trail");
        TrailRenderer lamaTrailRenderer = lamaInstanceGO.AddComponent<TrailRenderer>();
        lamaTrailRenderer.colorGradient = bulletTrailConfigSO.lamaBulletTrailColorGradient; //calling the SO on the GO, lamaBulletTrailColorGrad in the BulletTrailConfigSO
        lamaTrailRenderer.material = bulletTrailConfigSO.lamaBulletTrailMaterial; //Same here
        lamaTrailRenderer.widthCurve = bulletTrailConfigSO.lamaBulletTrailWidthAnimationCurve;
        lamaTrailRenderer.time = bulletTrailConfigSO.lamaBulletTrailDurationFloat;
        lamaTrailRenderer.minVertexDistance = bulletTrailConfigSO.lamaBulletTrailMinVertexDistanceFloat;

        lamaTrailRenderer.emitting = false;
        lamaTrailRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        return lamaTrailRenderer;
    }


}

