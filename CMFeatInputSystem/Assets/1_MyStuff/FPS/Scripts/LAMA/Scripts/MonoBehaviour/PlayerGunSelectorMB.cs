using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerGunSelectorMB : MonoBehaviour
{
    [SerializeField] LamaGunTypeEnum lamaGunLamaGunTypeEnum;
    [SerializeField] Transform lamaGunParentTransform;
    [SerializeField] List<GunConfigSO> lamaGunsGunConfigSOList;
    // [SerializeField] PlayerIK lamaInverseKinematics; UNLOCK WITH GITHUB IK
    [Space]
    [Header("Runtime Filled")] public GunConfigSO lamaActiveGunGunConfigSO;

    private void Start()
    {
        GunConfigSO lamaGunGunConfigSO = lamaGunsGunConfigSOList.Find(lamaGunConfigSO => lamaGunConfigSO.lamaTypeLamaGunTypeEnum == lamaGunLamaGunTypeEnum);

        if (lamaGunGunConfigSO == null)
        {
            Debug.LogError($"No GunScriptableObject found for GunType: {lamaGunGunConfigSO}");
            return;
        }

        lamaActiveGunGunConfigSO = lamaGunGunConfigSO;
        lamaGunGunConfigSO.LamaBulletSpawn(lamaGunParentTransform, this); // this MB

        // some magic for IK
        // Transform[] lamaAllChildren = lamaGunParentTransform.GetComponentsInChildren<Transform>();
        // InverseKinematics.LeftElbowIKTarget = lamaAllChildren.FirstOrDefault(lamaChild => lamaChild.name == "LeftElbow");
        // InverseKinematics.RightElbowIKTarget = lamaAllChildren.FirstOrDefault(lamaChild => lamaChild.name == "RightElbow");
        // InverseKinematics.LeftHandIKTarget = lamaAllChildren.FirstOrDefault(lamaChild => lamaChild.name == "LeftHand");
        // InverseKinematics.RightHandIKTarget = lamaAllChildren.FirstOrDefault(lamaChild => lamaChild.name == "RightHand");
    }



}
