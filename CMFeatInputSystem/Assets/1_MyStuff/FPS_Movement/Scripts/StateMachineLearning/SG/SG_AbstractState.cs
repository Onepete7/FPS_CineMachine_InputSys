using UnityEngine;

namespace SG
{
    public abstract class SG_State : MonoBehaviour
    {
        public abstract SG_State RunCurrentState();
    }
}