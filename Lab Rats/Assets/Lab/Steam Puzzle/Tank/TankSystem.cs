using System.Collections.Generic;
using UnityEngine;

namespace Lab.Steam_Puzzle.Tank
{
    public class TankSystem: MonoBehaviour
    {
        [SerializeField] private MoveObject plunger;
        [SerializeField] private ParticleSystem steam;
        ParticleSystem.EmissionModule steamEmmision;
        [Range(0, 100)]
        [SerializeField] private float gaugePercentageFullActivation = 75;
        [SerializeField] private float waterBoilingTemp = 100;

        [SerializeField] private float increasePressureSpeed = 0.001f;
        private float _pressure;
        private float _temperature;
        private float heat;
        private float oxygen;
        [SerializeField] private PressureGauge gauge;
        [SerializeField] private float _maxPressure;
        [SerializeField] private float _pressureLoss;


        /// <summary>
        ///  The pressure value of the tank 
        /// </summary>
        public float Pressure => _pressure;

        public readonly List<Coal> coalList = new();

        private void Start()
        {
            steamEmmision = steam.emission;
        }

        public void CoalAdded(Coal coal)
        {
            coal.SetHeating(heat, oxygen);
            coalList.Add(coal);
        }
        public void CoalRemoved(Coal coal)
        {
            coal.SetHeating(0, 0);
            coalList.Remove(coal);
        }

        public void SetHeat(float heat)
        {
            this.heat = heat;
            HeatCoal();
        }

        public void SetOxygen(float oxygen)
        {
            this.oxygen = oxygen;
            HeatCoal();
        }

        private void HeatCoal()
        {
            foreach (var coal in coalList)
            {
                coal.SetHeating(heat, oxygen);
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
            if (_temperature > waterBoilingTemp)
            {
                _pressure += Mathf.Pow(_temperature, 2) * increasePressureSpeed * Time.deltaTime;
                steamEmmision.enabled = true;
            }
            else
            {
                _pressure -= _pressureLoss * Time.deltaTime;
                steamEmmision.enabled = false;
            }

            _pressure = Mathf.Clamp(_pressure, 0, _maxPressure);
            gauge.UpdateRotation(_pressure);


            if ((_pressure / _maxPressure)* waterBoilingTemp >= gaugePercentageFullActivation)
            {
                plunger.canMove = true;
            }
            else
            {
                plunger.canMove = false;
            }
        }
    }
}