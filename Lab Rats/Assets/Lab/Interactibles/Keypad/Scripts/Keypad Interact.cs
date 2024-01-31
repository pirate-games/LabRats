using UnityEngine;

namespace Lab.Interactibles.Keypad.Scripts
{
    public class KeypadInteract : MonoBehaviour
    {
        [SerializeField]
        private KeypadFunctionality keypad;
        [SerializeField]
        private GameObject popupButton;
        [SerializeField]
        private int length;
        [SerializeField]
        private string code;

        private bool keypadOpen;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "XR Origin (XR Rig)")
            {
                if (!keypadOpen)
                {
                    popupButton.SetActive(true);
                }else if (keypad.closing)
                {
                    keypadOpen = false;
                    keypad.closing = false;
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.name == "XR Origin (XR Rig)")
            {
                keypadOpen = false;
                popupButton.SetActive(false);
                keypad.gameObject.SetActive(false);
                keypad.ClearAll();
            }
        }

        public void openKeypad()
        {
            keypadOpen = true;
            popupButton.SetActive(false);
            keypad.gameObject.SetActive(true);
            keypad.codeLength = length;
            keypad.correctCode = code;
            keypad.ClearAll();
        }
    }
}
