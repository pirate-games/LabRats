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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (keypad.correct)
        {
            if(collider.steelCount >= 2 && collider.mouldInside)
            {
                collider.steel1.SetActive(false);
                collider.steel2.SetActive(false);
                key.SetActive(true);
            }
        }
    }
}
