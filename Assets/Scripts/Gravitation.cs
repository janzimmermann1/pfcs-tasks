using UnityEngine;
using UnityEngine.Serialization;

public class Gravitation : MonoBehaviour
{
    public Transform center;
    public float massEarth = 5.972e24f;
    public float massComet = 1e8f;
    
    public float radiusEarth = 6.371f;
    public Vector3 initialVelocity;
    
    public float G = 6.67430e-11f;

    public bool showFlightCurve = false;
    
    public GameObject Explosion;
    
    // v1 = -2,0,1.5
    // v2 = -2,0,1
    private Vector3 _v;
    private float _scalingFactor = 1e6f;
    private float _dt;

    void Start()
    {
        _v = initialVelocity;
        _dt = Time.fixedDeltaTime;
    }
    

    void FixedUpdate()
    {
        // distance between earth and comet (scaled)
        Vector3 direction = (center.position - transform.position) * _scalingFactor;
        float distance = direction.magnitude;
        if (distance <= 0) return;
        
        // Calculate gravitationalForce: (G * mE * mC) / (distance^2) * direction
        Vector3 gravitationalForce = (G * massEarth * massComet / (distance * distance)) * direction.normalized;

        // Get acceleration on given force: (F = m * a => a = F / m)
        Vector3 acceleration = gravitationalForce / massComet;

        _v += acceleration * _dt;
        transform.position += _v * _dt;
        
        if (showFlightCurve)
        {
            SpawnFlightCurvePoint();
        }
        
        if (distance < radiusEarth * _scalingFactor)
        {
            BurnUp();
        }
    }

    private void BurnUp()
    {
        GameObject explosion = Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.1f);
        Destroy(explosion, 4f);
    }
    
    private void SpawnFlightCurvePoint()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = transform.position;
        sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); // Kleinere Kugel
        sphere.GetComponent<Renderer>().material.color = Color.green;

        Destroy(sphere, 10f);
    }
}