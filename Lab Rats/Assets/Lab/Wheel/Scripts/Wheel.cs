using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField]
    private Exothermicreaction exo;
    private Door door; //This can be changed to be any object that reacts to wheelturning

    [SerializeField]
    private float turnTreshold;

    private Quaternion originalRot;
    private Vector3 originalVec;

    // Start is called before the first frame update
    void Start()
    {
        //make sure wheel is in the same position at start always
        originalRot = Quaternion.identity;
        this.gameObject.transform.rotation = originalRot; 

        originalVec = this.gameObject.transform.rotation.eulerAngles; //startposition in angles
    }

    // Update is called once per frame
    void Update()
    {
        //make the wheel turn in only one direction (Important because of eulerangles)
        if (this.gameObject.transform.rotation.z <= 0)
        {
            this.gameObject.transform.rotation = originalRot;
        }
        //constrain rotation in y and z axis
        if(this.gameObject.transform.rotation.y != 0 || this.gameObject.transform.rotation.x != 0)
        {
            this.gameObject.transform.rotation = new Quaternion(originalRot.x, originalRot.y, this.gameObject.transform.rotation.z, originalRot.w);

        }

        //check if wheel is turning, no specific value
        if (this.gameObject.transform.rotation != originalRot)
        {
            Debug.Log("Rotating");
        }

        //check if wheel has turned a specific amount, treshold value in angles changed in inspector
        if (this.gameObject.transform.rotation.eulerAngles.z >= originalVec.z + turnTreshold)
        {
            exo.oxygenFlowing = true;


            //insert reaction to wheel turning, for example:
            /*            if (!door.isOpen)
                        {
                            door.Open();
                        }*/
        }
        else
        {
            exo.oxygenFlowing = false;
        }
        
    }
}
