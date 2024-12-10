using UnityEngine;

public class Gravitation : MonoBehaviour
{
    public Transform center;
    public float speed = 1f;
    public float radius;
    public float cyclesPerParentCircle = 3f;

    private float _dt;
    private Vector3 _v;
    private float _rotationSpeed;
    private bool _isStarted = false;
    
    void Start()
    {
        transform.position = new Vector3(center.position.x + radius, center.position.y, center.position.z);
        _dt = Time.fixedDeltaTime;
        _v = new Vector3(0, 0, speed);
        var circumference = 2 * radius * Mathf.PI;
        _rotationSpeed = circumference / speed / cyclesPerParentCircle;
    }

    void Update()
    {
        if (Input.GetKey("space"))
        {
            _isStarted = true;
        }
    }

    void FixedUpdate()
    {
        if (!_isStarted) return;
        RotateObject();
        LeapFrog();
    }

    private void RotateObject()
    {
        transform.Rotate(Vector3.up, 360 * _dt / _rotationSpeed);
    }

    private void LeapFrog()
    {
        var s_intermediate =  transform.position + _v * _dt / 2;
        var intermediateAcc = (center.position - s_intermediate).normalized * (speed * speed / radius);
        _v = _v + intermediateAcc * _dt;
        transform.position = s_intermediate + _v * _dt / 2;
    }
}