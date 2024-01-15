using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenFunctionality : MonoBehaviour
{
    [SerializeField]
    private OvenCollider collider;
    [SerializeField]
    private GameObject key, door, spinningPad;
    [SerializeField]
    private Light ovenLight1, ovenLight2;

    private float lightIntense = 0.4f, doorClosed = 88f;

    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (collider.steelCount >= 2 && collider.mouldInside && door.transform.rotation.eulerAngles.y >= doorClosed)
            {
                collider.steel1.SetActive(false);
                collider.steel2.SetActive(false);
                key.SetActive(true);
                isActive = false;
            }
        }
    }

    public void UpdateOven()
    {
        ovenLight1.intensity = lightIntense;
        ovenLight2.intensity = lightIntense;
        isActive = true;
    }
}
