using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mulitplayer.NetworkUI
{
    public class JoinScript : MonoBehaviour
    {
        public TMP_InputField input;
        public NetworkConnecter networkConnecter;

        [SerializeField] private Button submitButton;
        [SerializeField] private TMP_Text submitText;

        //Temporary Solution
        private const int MinimalCodeLength = 6;
    
        /// <summary>
        ///  Joins the game with the input code
        /// </summary>
        public void Joining()
        {
            var inputCode = input.text.ToFormattedCode();

            if (inputCode.Length == CodeFormatter.CodeLength) networkConnecter.Join(inputCode);
            else  Debug.Log("LOL code wrong, be better donut");
        }

        /// <summary>
        ///  Corrects the code to the correct format
        /// </summary>
        public void CorrectCode()
        {
            if (input.text.Length >= CodeFormatter.CodeLength)
            {
                input.text = input.text.ToFormattedCode();
            }
        }
    
        /// <summary>
        ///  Toggles the button interactable state based on the input length
        /// </summary>
        public void ToggleButton()
        {
            submitButton.interactable = input.text.Length >= CodeFormatter.CodeLength;
        
            submitText.color = input.text.Length >= MinimalCodeLength ? Color.black 
                : new Color(1, 1, 1, 0.5f);
        }
    }
}
