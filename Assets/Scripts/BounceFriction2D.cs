using UnityEngine;

public class BounceFriction2D : MonoBehaviour
{

    private Vector3 _a = new Vector3(0,-9.81f,0);
    private Vector3 _v = new Vector3(0,0,0);
    private float _sphereRadius;
    private float _dt;
    private GameObject _plane;

    private bool _isStarted = false;
    public float airCoeff = 0.1f;
    
    void Start()
    {
        _dt = Time.fixedDeltaTime;
        _plane = GameObject.Find("Plane");
        _sphereRadius = gameObject.GetComponent<SphereCollider>().radius;
    }

    void FixedUpdate()
    {
        if(!_isStarted) return;

        transform.position += _v * _dt + _a  * (0.5f * _dt * _dt);
        Vector3 airFriction = -airCoeff * _v;

        // Die Geschwindigkeit nimmt logischerweise ab...
        _v += (_a + airFriction) * _dt;
        
        Vector3 n = _plane.transform.up;

        if (Vector3.Dot(n, transform.position) < _sphereRadius)
        {
            _v = (_v.normalized - 2 * Vector3.Dot(n, _v.normalized) * n) * _v.magnitude;
        }

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
