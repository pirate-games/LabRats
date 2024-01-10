using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenFunctionality : MonoBehaviour
{
    [SerializeField]
    private OvenCollider collider;
    [SerializeField]
    private KeypadFunctionality keypad;
    [SerializeField]
    private GameObject key;
    [SerializeField]
    private Light ovenLight1, ovenLight2;

    private float lightIntense = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (keypad.correct)
        {
            ovenLight1.intensity = lightIntense;
            ovenLight2.intensity = lightIntense;
            if(collider.steelCount >= 2 && collider.mouldInside)
            {
                collider.steel1.SetActive(false);
                collider.steel2.SetActive(false);
                key.SetActive(true);
            }
        }
    }
}
