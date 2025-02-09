using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground2D.StationObject
{
    public abstract class Station : MonoBehaviour
    {
        // ������ �� �������, ������� ����� ���������
        public GameObject[] objectsToCheck;

        // ������ �� ���������, ������� ����� ��������/���������
        public BoxCollider colliderToControl;

        public void Update()
        {

            CheckObjectsAndControlCollider();
        }

        private void CheckObjectsAndControlCollider()
        {
            bool anyObjectActive = false;

            foreach (var obj in objectsToCheck)
            {
                if (obj.activeInHierarchy)
                {
                    anyObjectActive = true;
                    break;
                }
            }

            if (colliderToControl != null)
            {
                colliderToControl.enabled = !anyObjectActive;
            }
        }
    }
}
