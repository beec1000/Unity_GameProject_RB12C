using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float delay = 1f;

    private Vector3 offsetRight;
    private Vector3 offsetLeft;
    private float lowY;
    private bool facingRight;
    private float difX;

    private void Start()
    {
        lowY = transform.position.y;
        difX = transform.position.x - target.position.x;

        offsetRight = transform.position - target.position;
        offsetLeft = transform.position - target.position;
        offsetLeft.x -= 2 * difX;
        facingRight = true;
    }

    private void FixedUpdate()
    {

        Vector3 targetPos = target.localScale.x > 0
             ? target.position + offsetRight
             : target.position + offsetLeft;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            delay * Time.deltaTime);

        if (transform.position.y < lowY)
        {
            transform.position = new(transform.position.x, lowY, transform.position.z);
        }
    }
}
