using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float _lifetime=2f;
    private float _timestamp;

    private void Start()
    {
        if (_timestamp<=0)
        {
            _timestamp = Time.time + _lifetime;
        }
    }

    public void InitializeSelfDestruct(float lifetime)
    {
        _timestamp = lifetime + Time.time;
    }

    private void FixedUpdate()
    {
        if (_timestamp<Time.time)
        {
            Destroy(gameObject);
        }
    }
}
