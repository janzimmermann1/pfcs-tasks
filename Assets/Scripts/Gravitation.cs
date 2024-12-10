using UnityEngine;
using UnityEngine.Serialization;

public class Gravitation : MonoBehaviour
{
    public Transform center;
    public float massEarth = 5.972e24f;
    public float massComet = 1e11f;
    
    public float radiusEarth = 6371f;
    public Vector3 initialVelocity;
    
    public float G = 6.67430e-11f;

    private Vector3 _v;
    private float _scalingFactor = 1e6f;
    private bool isStarted = false;
    private float _dt;

    void Start()
    {
        _v = initialVelocity;
        _dt = Time.fixedDeltaTime;
    }

    void Update()
    {
        if (Input.GetKey("space"))
        {
            isStarted = true;
        }
    }

    void FixedUpdate()
    {
        if (!isStarted) return;

        // distance between earth and comet (scaled)
        Vector3 direction = (center.position - transform.position) * _scalingFactor;
        float distance = direction.magnitude;
        
        if (distance <= 0) return; // Schutz vor Division durch Null
        
        // Calculate gravitationalForce: (G * mE * mC) / (distance^2) * direction
        Vector3 gravitationalForce = (G * massEarth * massComet / (distance * distance)) * direction.normalized;

        // Get acceleration on given force: (F = m * a => a = F / m)
        Vector3 acceleration = gravitationalForce / massComet;

        _v += acceleration * _dt;
        transform.position += _v * _dt;
        
        if (distance < (radiusEarth / _scalingFactor))
        {
            BurnUp();
        }
    }

    private void BurnUp()
    {
        Debug.Log("Komet ist in der Erdatmosphäre verglüht!");
        Destroy(gameObject);
    }
}