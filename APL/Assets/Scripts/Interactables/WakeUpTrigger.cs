using System;
using UnityEngine;

namespace ALP.Interactables
{
    [RequireComponent(typeof(BoxCollider))]
    public class WakeUpTrigger : MonoBehaviour
    {
        public BoxCollider BoxCollider;

        #region UnityMethods

        private void Start()
        {
            BoxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out IPlayer player))
            {
                player.WakeUp();
            }

        }
        #endregion
    }
}
