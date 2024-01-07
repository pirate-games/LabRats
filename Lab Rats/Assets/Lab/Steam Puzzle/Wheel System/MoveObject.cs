using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private Vector3 beginPoint;
    [SerializeField] private Vector3 endPoint;
    [SerializeField] private float speed = 1;
    private float percentageDone = 1.0f; // Adjust the speed of lerping
    public bool canMove;

    private bool previousCanMoveState; // Keep track of the previous value of canMove

    private void Start()
    {
        previousCanMoveState = canMove;
    }

    private void Update()
    {
        if (canMove != previousCanMoveState)
        {
            // canMove state has changed
            OnCanMoveStateChanged();
            previousCanMoveState = canMove;
        }

        if (canMove)
        {
            LerpPosition(beginPoint, endPoint);

        }
        else
        {
            LerpPosition(endPoint, beginPoint);
        }
    }

    private void LerpPosition(Vector3 from, Vector3 to)
    {
        // Interpolate between 'from' and 'to' based on lerpSpeed
        transform.position = Vector3.Lerp(from, to, percentageDone);

        // Update percentageDone based on the distance between current position and target
        percentageDone += Time.deltaTime * speed; // You can adjust this value based on your desired speed

        // If the lerping is complete, reset percentageDone
        if (percentageDone >= 1.0f)
        {
            percentageDone = 1;
        }
    }

    private void OnCanMoveStateChanged()
    {
        percentageDone = 1- percentageDone;
    }
}
