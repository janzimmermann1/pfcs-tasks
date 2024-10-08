using UnityEngine;

public class Bounce2D : MonoBehaviour
{

    private Vector3 _a = new Vector3(0,-9.81f,0);
    private Vector3 _v = new Vector3(0,0,0);
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
        
        transform.position += _v * _dt + _a * (0.5f * _dt * _dt);
        _v += _a * _dt;
           
        Vector3 n = _plane.transform.up;

        if (Vector3.Dot(n, transform.position) < _sphereRadius)
        {
            _v = (_v.normalized - 2 * Vector3.Dot(n, _v.normalized) * n) * _v.magnitude;
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
