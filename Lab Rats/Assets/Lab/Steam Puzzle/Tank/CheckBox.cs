using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Lab.Steam_Puzzle.Tank
{
    public class CheckBox : MonoBehaviour
    {
        [SerializeField] private string objectTag;
        [SerializeField] private int amountOfObjects;

        public readonly List<GameObject> objectsInTank = new();
        public readonly List<CoalEmmsion> coalEmmsions = new();

        private bool _isActive;
        private string EmissionColor = "_EmissionColor";

        /// <summary>
        ///  Returns true if the amount of objects in the tank is equal to the amount
        /// of objects needed to activate the tank.
        /// </summary>
        public bool IsActive => _isActive;

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

        private void FixedUpdate()
        {
            _isActive = objectsInTank.Count == amountOfObjects;
        }

        public void heatUpCoal(float oxygen)
        {
            foreach (var o in coalEmmsions)
            {
                o.heatUp(oxygen);
            }
        }
        public void coolDownCoal()
        {
            foreach (var o in coalEmmsions)
            {
                o.cooldown();
            }
        }

        public float getTemp()
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
