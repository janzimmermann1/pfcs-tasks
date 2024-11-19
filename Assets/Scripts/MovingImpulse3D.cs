using UnityEngine;

public class MovingImpulse3D: MonoBehaviour
{
    [SerializeField] public float Speed = 0f;
    [SerializeField] public float Mass = 1f;
    [SerializeField] public MovingImpulse3D OtherCube;
    
    private Vector3 _v = Vector3.zero;
    private Vector3 _a = Vector3.zero;
    private float _dt;
    private float _cubeSize = 1f;
    private bool _isCollided = false;
    
    void Start()
    {
        _dt = Time.fixedDeltaTime;
        _cubeSize = transform.localScale.x;
        _v = transform.forward * Speed; // Geschwindigkeitsvektor in die Richtung des Cubes
    }

    void FixedUpdate()
    {
        var t = _dt / 2;
        transform.position += _v * t;
        
        _v += _a * _dt;
        transform.position += _v * t;

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
        Vector3 combinedVelocity = (Mass * _v + OtherCube.Mass * OtherCube._v) / totalMass;

        _v = combinedVelocity;
        OtherCube._v = combinedVelocity;
    }
}
