using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground2D.Forms.CollectItems
{
    public abstract class Forms : MonoBehaviour
    {
        [field: SerializeField] public int BoostValue { get; private set; }

        private event Action _pickedUp;

        public void Initialize(Action pickedUp)
        {
            _pickedUp = pickedUp;
        }

        public void Pickup()
        {
            _pickedUp?.Invoke();
        }
    }
}