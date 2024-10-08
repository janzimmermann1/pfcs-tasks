using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{

    private float _acceleration = 9.81f;
    private float _velocity = 0f;
    private float _sphereRadius;
    
    void Start()
    {
        _sphereRadius = gameObject.GetComponent<SphereCollider>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= _sphereRadius)
        { 
            _velocity *= -1;
        }
        var newHeight = _velocity * Time.deltaTime + transform.position.y - 0.5f * _acceleration * (Time.deltaTime * Time.deltaTime);
        _velocity += -1 * _acceleration * Time.deltaTime;
        
        Debug.Log("height previous=" + transform.position + "; new=" + newHeight + "; velocity=" + _velocity);
        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
    }
}
