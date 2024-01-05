using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    [SerializeField] private string Tag;
    [SerializeField] private int amountOfObjects;
    [SerializeField] private int currentAmount;
    private bool _isActive = false;

    public bool isActive { get { return _isActive; } }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag)
        {
            currentAmount++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tag)
        {
            currentAmount--;
        }
    }

    private void FixedUpdate()
    {
        if (currentAmount == amountOfObjects)
        {
            _isActive = true;
        }
        else
        {
            _isActive = false;
        }
    }
}
