using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground2D.CollectItems.Collector
{
    public class FormsCollector : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _collider;

        public float CurrentRadius => _collider.radius;

        public void ChangeCollectRadius(float amount)
        {
            _collider.radius += amount;
        }
    }
}
