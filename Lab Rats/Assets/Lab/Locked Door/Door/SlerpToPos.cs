using UnityEngine;

public class SlerpToPos : MonoBehaviour
{
    [SerializeField]
    Transform startTransform;
    [SerializeField]
    Transform endTransform;

    [SerializeField]
    float time = 0;

    public bool Moving { get; set; }

    float _time = 0;

    void Start()
    {
        if (!startTransform) startTransform = transform;
    }

    void FixedUpdate()
    {
        if (!startTransform || !endTransform || !Moving || _time > time) return;

        _time += Time.fixedDeltaTime;

        var scaledTime = _time / time;

        var newPos = Vector3.Slerp(startTransform.position, endTransform.position, scaledTime);
        var newRot = Quaternion.Slerp(startTransform.rotation, endTransform.rotation, scaledTime);

        transform.SetPositionAndRotation(newPos, newRot);
    }
    
    public void StartMoving(bool overrideTransform = false)
    {
        if (overrideTransform)
        {
            startTransform = transform;
        }
        
        Moving = true;
    }
}
