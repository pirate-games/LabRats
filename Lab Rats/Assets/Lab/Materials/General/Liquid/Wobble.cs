using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

//https://www.youtube.com/watch?v=DKSpgFuKeb4
public class Wobble : NetworkBehaviour
{
    Renderer rend;
    Vector3 lastPos;
    Vector3 velocity;
    Vector3 lastRot;
    Vector3 angularVelocity;
    public float MaxWobble = 0.03f;
    public float WobbleSpeed = 1f;
    public float Recovery = 1f;
    float wobbleAmountX;
    float wobbleAmountZ;
    float wobbleAmountToAddX;
    float wobbleAmountToAddZ;
    float pulse;
    float time = 0.5f;

    NetworkVariable<Color> thisColor = new NetworkVariable<Color>();
    NetworkVariable<float> fillHeight = new NetworkVariable<float>();
    NetworkVariable<float> intensity = new NetworkVariable<float>();
    // Use this for initialization

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Debug.Log("Testing");
        rend = GetComponent<Renderer>();

        if (IsHost)
        {
            CalculateValues();
            SetValuesClientRpc();
        }
        else
        {
            SetValuesClientRpc();
        }
    }

    [ClientRpc]
    private void SetValuesClientRpc()
    {
        Debug.Log("SetValue");
        rend.material.SetFloat("_EmissionIntemsity", intensity.Value);
        rend.material.SetFloat("_Fill", fillHeight.Value);
        rend.material.SetColor("_EmissonColor", thisColor.Value);  // Fix the typo here
        rend.material.SetColor("_LiquidColor", thisColor.Value);
        rend.material.SetColor("_SurfaceColor", thisColor.Value);
    }
    private void CalculateValues()
    {
        float randomHue = Random.Range(0f, 1f);
         intensity.Value = Random.Range(3, 9);
         fillHeight.Value = Random.Range(0.485f, 0.52f);
        // Set saturation and value to their maximum for full color strength
        thisColor.Value = Color.HSVToRGB(randomHue, 1f, 1f);

    }
    private void Update()
    {
        time += Time.deltaTime;

        // decrease wobble over time
        wobbleAmountToAddX = Mathf.Lerp(wobbleAmountToAddX, 0, Time.deltaTime * (Recovery));
        wobbleAmountToAddZ = Mathf.Lerp(wobbleAmountToAddZ, 0, Time.deltaTime * (Recovery));

        // make a sine wave of the decreasing wobble
        pulse = 2 * Mathf.PI * WobbleSpeed;
        wobbleAmountX = wobbleAmountToAddX * Mathf.Sin(pulse * time);
        wobbleAmountZ = wobbleAmountToAddZ * Mathf.Sin(pulse * time);

        // send it to the shader
        rend.material.SetFloat("_WobbleX", wobbleAmountX);
        rend.material.SetFloat("_WobbleZ", wobbleAmountZ);
        

        

        // velocity
        velocity = (lastPos - transform.position) / Time.deltaTime;
        angularVelocity = transform.rotation.eulerAngles - lastRot;


        // add clamped velocity to wobble
        wobbleAmountToAddX += Mathf.Clamp((velocity.x + (angularVelocity.z * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);
        wobbleAmountToAddZ += Mathf.Clamp((velocity.z + (angularVelocity.x * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);

        // keep last position
        lastPos = transform.position;
        lastRot = transform.rotation.eulerAngles;
    }



}
