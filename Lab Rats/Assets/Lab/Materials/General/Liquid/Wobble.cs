using Audio;
using Unity.Netcode;
using UnityEngine;

//https://www.youtube.com/watch?v=DKSpgFuKeb4

namespace Lab.Materials.General.Liquid
{
    [RequireComponent(typeof(AudioSource))]
    public class Wobble : NetworkBehaviour
    {
        private Renderer _rend;
        private Vector3 _lastPos;
        private Vector3 _velocity;
        private Vector3 _lastRot;
        private Vector3 _angularVelocity;
        private AudioSource _audioSource;
        
        public float maxWobble = 0.03f;
        public float wobbleSpeed = 1f;
        public float recovery = 1f;
        private float _wobbleAmountX;
        private float _wobbleAmountZ;
        private float _wobbleAmountToAddX;
        private float _wobbleAmountToAddZ;
        private float _pulse;
        private float _time = 0.5f;
        
        [SerializeField] private AudioEvent wobbleSound;

        private readonly NetworkVariable<Color> _thisColor = new();
        private readonly NetworkVariable<float> _fillHeight = new();
        private readonly NetworkVariable<float> _intensity = new();
        
        private static readonly int EmissionIntensity = Shader.PropertyToID("_EmissionIntemsity");
        private static readonly int Fill = Shader.PropertyToID("_Fill");
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissonColor");
        private static readonly int LiquidColor = Shader.PropertyToID("_LiquidColor");
        private static readonly int SurfaceColor = Shader.PropertyToID("_SurfaceColor");
        private static readonly int WobbleX = Shader.PropertyToID("_WobbleX");
        private static readonly int WobbleZ = Shader.PropertyToID("_WobbleZ");

        private void Start()
        {
            _rend = GetComponent<Renderer>();
            _audioSource = GetComponent<AudioSource>();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            _rend = GetComponent<Renderer>();

            if (IsHost)
            {
                CalculateValuesServerRpc();
                SetValue();
            }
            else
            {
                SetValue();
            }
        }

        private void SetValue()
        {
            _rend.material.SetFloat(EmissionIntensity, _intensity.Value);
            _rend.material.SetFloat(Fill, _fillHeight.Value);
            _rend.material.SetColor(EmissionColor, _thisColor.Value);  
            _rend.material.SetColor(LiquidColor, _thisColor.Value);
            _rend.material.SetColor(SurfaceColor, _thisColor.Value);
        }

        [ServerRpc]
        private void CalculateValuesServerRpc()
        {
            var randomHue = Random.Range(0f, 1f);
            
            _intensity.Value = Random.Range(3, 9);
            _fillHeight.Value = Random.Range(0.485f, 0.52f);
            
            // Set saturation and value to their maximum for full color strength
            _thisColor.Value = Color.HSVToRGB(randomHue, 1f, 1f);
            
            // when the wobble starts, play the sound
            wobbleSound.Play(_audioSource);
        }
        private void FixedUpdate()
        {
            _time += Time.deltaTime;

            // decrease wobble over time
            _wobbleAmountToAddX = Mathf.Lerp(_wobbleAmountToAddX, 0, Time.deltaTime * (recovery));
            _wobbleAmountToAddZ = Mathf.Lerp(_wobbleAmountToAddZ, 0, Time.deltaTime * (recovery));

            // make a sine wave of the decreasing wobble
            _pulse = 2 * Mathf.PI * wobbleSpeed;
            _wobbleAmountX = _wobbleAmountToAddX * Mathf.Sin(_pulse * _time);
            _wobbleAmountZ = _wobbleAmountToAddZ * Mathf.Sin(_pulse * _time);

            // send it to the shader
            _rend.material.SetFloat(WobbleX, _wobbleAmountX);
            _rend.material.SetFloat(WobbleZ, _wobbleAmountZ);

            // velocity
            _velocity = (_lastPos - transform.position) / Time.deltaTime;
            _angularVelocity = transform.rotation.eulerAngles - _lastRot;


            // add clamped velocity to wobble
            _wobbleAmountToAddX += Mathf.Clamp((_velocity.x + (_angularVelocity.z * 0.2f)) * maxWobble, -maxWobble, maxWobble);
            _wobbleAmountToAddZ += Mathf.Clamp((_velocity.z + (_angularVelocity.x * 0.2f)) * maxWobble, -maxWobble, maxWobble);

            // keep last position
            _lastPos = transform.position;
            _lastRot = transform.rotation.eulerAngles;
        }



    }
}
