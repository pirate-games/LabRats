using UnityEngine;
using UnityEngine.Events;

namespace Lab.General_assets.Scripts.Office
{
    public class OpenDoors : MonoBehaviour
    {
    
        [SerializeField] private UnityEvent onPlayersReady;
        public bool opening { private get; set; }
    
        [SerializeField] private GameObject doorLeft, doorRight;
        [SerializeField] private OfficeButton buttonLeft, buttonRight;
        private Quaternion _doorLTargetRotation, _doorRTargetRotation;


        private void Start()
        {
            _doorLTargetRotation = Quaternion.Euler(0,-130,0);
            _doorRTargetRotation = Quaternion.Euler(0, 130,0);
        
            buttonLeft.onButtonPress.AddListener(ArePlayersReady);
            buttonRight.onButtonPress.AddListener(ArePlayersReady);
        }
    
        /// <summary>
        /// Whenever players are ready, this method calls for the intro audio to be played.
        /// </summary>
        public void ArePlayersReady()
        {
            if (buttonLeft.hasBeenPressed && buttonRight.hasBeenPressed)
            {
                onPlayersReady.Invoke();
            }
        }
    
        private void FixedUpdate()
        {
            if (opening && doorLeft.transform.localRotation != _doorLTargetRotation
                        && doorRight.transform.localRotation != _doorRTargetRotation)
            {
                Open();
            }
        }


        private void Open()
        {
            doorLeft.transform.localRotation =
                Quaternion.Slerp(doorLeft.transform.localRotation, _doorLTargetRotation, Time.deltaTime);
            doorRight.transform.localRotation =
                Quaternion.Slerp(doorRight.transform.localRotation, _doorRTargetRotation, Time.deltaTime);
        }
    }
}
