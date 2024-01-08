using Lab.Steam_Puzzle.Wheel_System;
using Unity.VRTemplate;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using Unity.Netcode;

namespace Lab.Steam_Puzzle.Tank
{
    public class TankSystem: NetworkBehaviour
    {
        [SerializeField] private CheckBox checkBox;
        [SerializeField] private ActivateParticleOnTrigger oxygen;
        [SerializeField] private XRKnob knob;
        [SerializeField] private MoveObject plunger;
        [SerializeField] private ParticleSystem steam;
        [Range(0, 100)] [SerializeField] private float GaugePercentageFullActivation = 75;

        [SerializeField] private float increasePressureSpeed = 0.000001f;

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

        private void FixedUpdate()
        {
            ActivateTankSystem();
        }
        private void ActivateTankSystem()
        {
            var checkKnobTurned = knob.value >= 1;

            if (oxygen.IsActivated && checkKnobTurned)
            {
                _temperature = checkBox.GetTemp()*(1-oxygen.Wheel.value);
                _pressure += (checkBox.GetTemp() * checkBox.GetTemp() * checkBox.GetTemp()) * increasePressureSpeed;
                if (_pressure > _maxPressure)
                {
                    _pressure = _maxPressure;
                }
                gauge.UpdateRotation(_pressure);
                checkBox.HeatUpCoal(oxygen.Wheel.value);
            }
            else
            {

                checkBox.CoolDownCoal();
                _pressure -= _pressureLoss * increasePressureSpeed;
                if (_pressure < 0)
                {
                    _pressure = 0;
                }
                gauge.UpdateRotation(_pressure);
            }
            if (100 / _maxPressure * _pressure >= GaugePercentageFullActivation && goUp)
            {
                plunger.canMove = true;
                goUp = false;
                goDown = true;
            }
            else if(100 / _maxPressure * _pressure <= GaugePercentageFullActivation && goDown)
            {
                plunger.canMove = false;

                goUp = true;
                goDown = false;
            }
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