using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

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

