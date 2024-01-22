using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.Events;

namespace Lab.Steam_Puzzle.Tank
{
    [RequireComponent(typeof(AudioSource))]
    public class TankSystem: MonoBehaviour
    {
        [SerializeField] private float waterBoilingTemp = 100;
        [SerializeField] private float increasePressureSpeed = 0.001f;
        [SerializeField] private float maxPressure = 330;
        [SerializeField] private float pressureLoss = 50;
        [SerializeField, Range(0, 100)] private float gaugePercentageFullActivation = 75;

        [SerializeField] UnityEvent onStartBoiling;
        [SerializeField] UnityEvent onStopBoiling;
        [SerializeField] UnityEvent<float> onPressureValueChanged;
        [SerializeField] UnityEvent<bool> onMovePlunger;
        
        [Header("Sound")]
        [SerializeField] private AudioEvent boilingSound;

        private float pressure;
        private float temperature;
        private float heat;
        private float oxygen;
        private bool boiling;
        private bool plungerMoving;
        private AudioSource _audioSource;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        ///  The pressure value of the tank 
        ///  Calls onPressureValueChanged event when set
        /// </summary>
        public float Pressure
        {
            get { return pressure; }
            set
            {
                pressure = value;
                onPressureValueChanged.Invoke(value);
            }
        }

        public readonly List<Coal> coalList = new();

        private void FixedUpdate()
        {
            temperature = GetCoalTemp();

            UpdatePressure();
            UpdatePlunger();
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

        private void UpdatePressure()
        {
            if (boiling)
            {
                if (temperature < waterBoilingTemp)
                {
                    boiling = false;
                    onStopBoiling.Invoke();
                }
                else
                {
                    // Exponentially increase pressure based on temperature
                    pressure += Mathf.Pow(temperature, 2) * increasePressureSpeed * Time.deltaTime;
                }
            }
            else
            {
                if (temperature >= waterBoilingTemp)
                {
                    boiling = true;
                    onStartBoiling.Invoke();
                    boilingSound.Play(_audioSource);
                }
                else
                {
                    // Decrease pressure with time
                    pressure -= pressureLoss * Time.deltaTime;
                    boilingSound.Stop(_audioSource, true);
                }
            }

            // Clamp pressure to be within a specified range
            Pressure = Mathf.Clamp(pressure, 0, maxPressure);
        }

        private void UpdatePlunger()
        {
            if (plungerMoving)
            {
                if ((pressure / maxPressure) * waterBoilingTemp < gaugePercentageFullActivation)
                {
                    // Move plunger down
                    plungerMoving = false;
                    onMovePlunger.Invoke(false);
                }
            }
            else
            {
                if ((pressure / maxPressure) * waterBoilingTemp >= gaugePercentageFullActivation)
                {
                    // Move plunger up
                    plungerMoving = true;
                    onMovePlunger.Invoke(true);
                }
            }
        }
    }
}