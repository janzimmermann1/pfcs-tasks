using UnityEngine;

public class Moving3D: MonoBehaviour
{
    private Vector3 _v = Vector3.zero;
    private float _sphereRadius;
    private float _dt;
    private GameObject _plane;

    private bool _isStarted = false;
    
    void Start()
    {
        _dt = Time.fixedDeltaTime;
        _plane = GameObject.Find("Plane");
        _sphereRadius = gameObject.GetComponent<SphereCollider>().radius;
    }

    void FixedUpdate()
    {
        if(!_isStarted) return;

        Vector3 g = new Vector3(0, -9.81f, 0);
        Vector3 n = _plane.transform.up;
        float n_x = n.x;
        float n_y = n.y;
        float n_z = n.z;
        
        // Formel für den steilsten Abwärtsvektor
        Vector3 steepestDescent = new Vector3(
            n_x / n_y,
            -(n_x * n_x + n_z * n_z) / (n_y * n_y),
            n_z / n_y
        );
        
        Vector3 a = steepestDescent * g.magnitude;
        _v += a * _dt;
        transform.position += _v * _dt;
        
        KeepBallOnPlane();
    }
    
    void KeepBallOnPlane()
    {
        Vector3 posElement = transform.position;
        Vector3 nPlane = _plane.transform.up;
        Vector3 posPlane = _plane.transform.position;

        // Abstand des Elementzentrums zur Ebene
        float distance = Vector3.Dot(nPlane, posElement - posPlane);
        
        if (distance < _sphereRadius)
        {
            // Korrigiere die Y-Position des Elements, durch Verschiebung auf der Ebene
            transform.position = posElement - nPlane * (distance - _sphereRadius);
        }
    }
    
    void Update()
    {
        if (Input.GetKey("space"))
        {
            _isStarted = true;
        }

        if (Input.GetKey("up"))
        {
            _plane.transform.Rotate(new Vector3(0.1f, 0, 0));
        }

        if (Input.GetKey("down"))
        {
            _plane.transform.Rotate(new Vector3(-0.1f, 0, 0));
        }
        if (Input.GetKey("left"))
        {
            _plane.transform.Rotate(new Vector3(0, 0, 0.1f));
        }

        if (Input.GetKey("right"))
        {
            _plane.transform.Rotate(new Vector3(0, 0, -0.1f));
        }

    }
}
