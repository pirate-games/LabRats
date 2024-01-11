using Lab.Steam_Puzzle.Wheel_System;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace Lab.Steam_Puzzle.Tank
{
    public class TankSystem: MonoBehaviour
    {
        [SerializeField] private MoveObject plunger;
        [SerializeField] private ParticleSystem steam;
        [Range(0, 100)]
        [SerializeField] private float GaugePercentageFullActivation = 75;

        [SerializeField] private float increasePressureSpeed = 0.000001f;
        [SerializeField] private float oxygenHeatMult;
        private bool goUp = true, goDown = true;
        private float _pressure;
        private float _temperature;
        [SerializeField] private PressureGauge gauge;
        [SerializeField]private float _maxPressure;
        [SerializeField] private float _pressureLoss;


        /// <summary>
        ///  The pressure value of the tank 
        /// </summary>
        public float Pressure => _pressure;

        public readonly List<Coal> coalList = new();

        public void CoalAdded(Coal coal)
        {
            this.coalList.Add(coal);
        }
        public void CoalRemoved(Coal coal)
        {
            coal.Heating = false;
            this.coalList.Remove(coal);
        }

        public void HeatCoal(float heat)
        {
            var hasHeat = heat >= 1;
            foreach (var coal in coalList)
            {
                coal.Heating = hasHeat;
            }
        }
        public void HasOxygen(float flow)
        {
            var hasOxygen = flow >= 1;
            foreach(var coal in coalList)
            {
                coal.HasOxygen = hasOxygen;
            }
        }
        public float GetCoalTemp()
        {
            float temp = 0;
            foreach (var coal in coalList)
            {
                temp += coal.Temperature;
            }
            return temp;
        }

        private void FixedUpdate()
        {
            _temperature = GetCoalTemp();
            //exponetionally increases effect of adding extra coal
            _pressure += (Mathf.Pow(_temperature, 3)) * increasePressureSpeed;
            Mathf.Clamp(_temperature, 0, _maxPressure);
            gauge.UpdateRotation(_pressure);

            //set gauge 
            if (100 / _maxPressure * _pressure >= GaugePercentageFullActivation && goUp)
            {
                plunger.canMove = true;
                goUp = false;
                goDown = true;
            }
            else if (100 / _maxPressure * _pressure <= GaugePercentageFullActivation && goDown)
            {
                plunger.canMove = false;

                goUp = true;
                goDown = false;
            }

            //particlesystem activate
            if (100 / _maxPressure * _pressure > 0)
            {
                // Stop and clear the particle system
                var emmision = steam.emission;
                emmision.enabled = true;
            }
            else if (100 / _maxPressure * _pressure <= 0)
            {
                // Stop the particle system
                var emmision = steam.emission;
                emmision.enabled = false;
            }
        }
    }
}