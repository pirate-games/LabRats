using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Lab.Steam_Puzzle.Tank
{
    public class CheckBox : NetworkBehaviour
    {
        [SerializeField] private string objectTag;
        [SerializeField] private int amountOfObjects;

        public List<GameObject> objectsInTank = new();
        public readonly List<CoalEmmsion> coalEmmsions = new();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(objectTag))
            {
                objectsInTank.Add(other.gameObject);
                other.gameObject.GetComponent<CoalEmmsion>().coolingDown = false;
                coalEmmsions.Add(other.gameObject.GetComponent<CoalEmmsion>());

            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(objectTag))
            {
                objectsInTank.Remove(other.gameObject);
                other.gameObject.GetComponent<CoalEmmsion>().coolingDown = true;
                coalEmmsions.Remove(other.gameObject.GetComponent<CoalEmmsion>());

            }
        }

        public void HeatUpCoal(float oxygen)
        {
            foreach (var o in coalEmmsions)
            {
                o.HeatUp(oxygen);
            }
        }
        public void CoolDownCoal()
        {
            foreach (var o in coalEmmsions)
            {
                o.Cooldown();
            }
        }

        public float GetTemp()
        {
            float temp = 0;
            foreach (var o in coalEmmsions)
            {
                temp += o.Temperature;
            }
            return temp;       
        }
    }
}
