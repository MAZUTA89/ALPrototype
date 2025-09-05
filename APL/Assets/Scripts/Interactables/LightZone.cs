using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ALP.Interactables
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class LightZone : MonoBehaviour
    {
        Collider _collider;
        Rigidbody _rb;

        #region UnityMethods
        private void Start()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
        }
        #endregion

        public void OnEnterObstacle()
        {
            Destroy(gameObject);
        }
    }
}
