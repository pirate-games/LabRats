using TMPro;
using UnityEngine;
using UnityEngine.Video;

namespace Lab.Interactibles.Telescope.Scripts
{
    public class MircoSample : MonoBehaviour
    {
        public TMP_Text EditText;
        public VideoPlayer videoInput;
        public VideoClip clip;

        private void Start()
        {
            EditText.text = clip.name;
            videoInput.clip = clip;
        }
    }
}

