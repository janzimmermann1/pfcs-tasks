using UnityEngine;

public class MovingFriction3D: MonoBehaviour
{
    private Vector3 _v = Vector3.zero;
    private float _cubeSize;
    private float _dt;
    private GameObject _plane;
    
    // ice at 0�C, source: https://www.engineeringtoolbox.com/friction-coefficients-d_778.html
    private float muIceStatic = 0.1f;
    private float muIceKinetic = 0.02f;
    public float Mass = 1f;
    public bool HasFriction = true;
    
    public float cw = 1.05f;
    public float A = 1;
    public float rho = 1.225f;
    
    void Start()
    {
        _dt = Time.fixedDeltaTime;
        _plane = GameObject.Find("Plane");
        _cubeSize = 0.5f;
    }

    void FixedUpdate()
    {
        var t = _dt / 2;
        transform.position += _v * t;

        Vector3 g = new Vector3(0, -9.81f, 0);
        Vector3 n = _plane.transform.up;

        float N = Mass * Vector3.Dot(-g.normalized, n.normalized) * g.magnitude;

        Vector3 gParallel = Vector3.ProjectOnPlane(g, n);
        Vector3 a = gParallel / Mass;

        if (HasFriction)
        {
            if (_v.magnitude == 0)
            {
                // Statische Reibung
                float maxStaticFriction = muIceStatic * N;
                Vector3 neededForce = -gParallel;
                if (neededForce.magnitude <= maxStaticFriction)
                {
                    a = Vector3.zero; // Keine Bewegung
                }
                else
                {
                    Vector3 staticFriction = maxStaticFriction * -neededForce.normalized;
                    a += staticFriction / Mass;
                }
            }
            else
            {
                // Kinetische Reibung
                Vector3 kineticFriction = muIceKinetic * N * -_v.normalized;
                a += kineticFriction / Mass;
            }

            // Luftwiderstand
            float vMag = _v.magnitude;
            if (vMag > 0)
            {
                Vector3 airFriction = 0.5f * cw * rho * A * vMag * vMag * -_v.normalized;
                a += airFriction / Mass;
            }
        }
        
        _v += a * _dt;
        transform.position += _v * t;
        KeepBallOnPlane();
    }

    
    void KeepBallOnPlane()
    {
        Vector3 posElement = transform.position;
        Vector3 nPlane = _plane.transform.up;
        Vector3 posPlane = _plane.transform.position;

        // Abstand des Elementzentrums zur Ebene
        float distance = Vector3.Dot(nPlane, posElement - posPlane);
        
        transform.position = posElement - nPlane * (distance - _cubeSize);
    }
    
    void Update()
    {

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
        
        transform.rotation = _plane.transform.rotation;
    }
}
