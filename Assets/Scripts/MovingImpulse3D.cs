using UnityEngine;

public class MovingImpulse3D: MonoBehaviour
{
    [SerializeField] public Vector3 Velocity = Vector3.zero;
    [SerializeField] public float Mass = 1f;
    [SerializeField] public MovingImpulse3D OtherCube;
    
    private Vector3 _a = Vector3.zero;
    private float _dt;
    private float _cubeSize = 1f;
    private bool _isCollided = false;
    
    void Start()
    {
        _dt = Time.fixedDeltaTime;
        _cubeSize = transform.localScale.x;
    }

    void FixedUpdate()
    {
        var t = _dt / 2;
        transform.position += Velocity * t;
        
        Velocity += _a * _dt;
        transform.position += Velocity * t;

        if (!_isCollided)
        {
            CheckCollision();
        }
    }
    
    private void CheckCollision()
    {
        if (OtherCube is not null && Vector3.Distance(transform.position, OtherCube.transform.position) <= _cubeSize)
        {
            OnCollision();
        }
    }
    
    private void OnCollision()
    {
        _isCollided = true;
        OtherCube._isCollided = true;
        
        float totalMass = Mass + OtherCube.Mass;
        Vector3 combinedVelocity = (Mass * Velocity + OtherCube.Mass * OtherCube.Velocity) / totalMass;

        Velocity = combinedVelocity;
        OtherCube.Velocity = combinedVelocity;
    }
}
