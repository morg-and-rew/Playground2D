using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground2D.СonsumerOblect
{
    public class Сonsumer : MonoBehaviour
    {
        [field: SerializeField] public float _biofluidForm { get; private set; } = 5f;
        private bool _isActive;

        public void Initialize()
        {

        }
   }
}
