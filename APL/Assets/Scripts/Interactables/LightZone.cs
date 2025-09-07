using UnityEngine;

namespace ALP.Interactables
{
    public class LightZone : MonoBehaviour
    {
        public void OnEnterObstacle()
        {
            Destroy(gameObject);
        }
    }
}
