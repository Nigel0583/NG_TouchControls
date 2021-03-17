using UnityEngine;

public class TiltObject : MonoBehaviour
{
    private Rigidbody _rb;
    private float _dirX;
    private const float TiltMoveSpeed = 20f;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        _dirX = Input.acceleration.x * TiltMoveSpeed;
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_dirX, 0f);
    }
}