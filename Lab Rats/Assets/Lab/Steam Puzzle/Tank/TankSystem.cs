using Lab.Steam_Puzzle.Wheel_System;
using Unity.VRTemplate;
using UnityEngine;

namespace Lab.Steam_Puzzle.Tank
{
    public class TankSystem: MonoBehaviour
    {
        [SerializeField] private CheckBox checkBox;
        [SerializeField] private ActivateParticleOnTrigger oxygen;
        [SerializeField] private XRKnob knob;

        [Header("How slow should the temp increase")] [Range(0, 1)]
        [SerializeField] private float increaseTempSpeed = 0.01f;
        
        private float _pressure;
        private float _temperature;

        /// <summary>
        ///  The pressure value of the tank 
        /// </summary>
        public float Pressure => _pressure;

        private void ActivateTankSystem()
        {
            var checkKnobTurned = knob.value >= 1;
            var checkAmountCoal = checkBox.objectsInTank.Count;

            if (!oxygen.IsActivated || !checkKnobTurned) return;
            
            _temperature += (checkAmountCoal * checkAmountCoal) * increaseTempSpeed;
                
            checkBox.UpdateCoalProperties();
        }
    }
}