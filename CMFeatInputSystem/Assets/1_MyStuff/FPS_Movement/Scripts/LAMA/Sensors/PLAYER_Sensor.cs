using UnityEngine;

namespace LAMA.Sensors
{
    [RequireComponent(typeof(SphereCollider))]

    public class PLAYER_Sensor : MonoBehaviour
    {
        public delegate void PlayerEnterEvent(Transform player);
        public delegate void PLayerExitEvent(Vector3 lastKnownPosition);

        public event PlayerEnterEvent OnPlayerEnter;
        public event PLayerExitEvent OnPlayerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out OP7_PlayerMovement player))
            {
                OnPlayerEnter?.Invoke(player.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out OP7_PlayerMovement player))
            {
                OnPlayerExit?.Invoke(other.transform.position);
            }
        }

    }

}