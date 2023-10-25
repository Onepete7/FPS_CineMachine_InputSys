using UnityEngine;

[CreateAssetMenu(fileName = "BulletTrail Config", menuName = "Guns/BulletTrail Config", order = 4)]

public class BulletTrailConfigSO : ScriptableObject
{
    public Material lamaBulletTrailMaterial;
    public AnimationCurve lamaBulletTrailWidthAnimationCurve;
    public float lamaBulletTrailDurationFloat = 0.5f;
    public float lamaBulletTrailMinVertexDistanceFloat = 0.1f;

    public Gradient lamaBulletTrailColorGradient;
    public float lamaBulletTrailMissDistanceFloat = 100.0f; //how far it goes if you miss
    public float lamaBulletTrailSimulationSpeedFloat = 100.0f; //how fast the trail goes to the maximum distance (m/s)
}
