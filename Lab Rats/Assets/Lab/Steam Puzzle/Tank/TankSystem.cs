﻿using Lab.Steam_Puzzle.Wheel_System;
using Unity.VRTemplate;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace Lab.Steam_Puzzle.Tank
{
    public class TankSystem: MonoBehaviour
    {
        [SerializeField] private CheckBox checkBox;
        [SerializeField] private ActivateParticleOnTrigger oxygen;
        [SerializeField] private XRKnob knob;
        [SerializeField] private MoveObject plunger;
        [SerializeField] private ParticleSystem steam;
        [Range(0, 100)]
        [SerializeField] private float GaugePercentageFullActivation = 75;

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
                _temperature = checkBox.getTemp()*1-oxygen.wheel.value;
                _pressure += (checkBox.getTemp() * checkBox.getTemp() * checkBox.getTemp()) * increasePressureSpeed;
                if (_pressure > _maxPressure)
                {
                    _pressure = _maxPressure;
                }
                gauge.UpdateRotation(_pressure);
                checkBox.heatUpCoal(oxygen.wheel.value);
            }
            else
            {

                checkBox.coolDownCoal();
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
            if (100 / _maxPressure * _pressure > 0 && steam.isStopped)
            {
                steam.Play();
            }
            else if(100 / _maxPressure * _pressure <= 0 && steam.isPlaying)
            {
                steam.Stop();
            }
        }
    }
}