using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(-1,1)] float movementFactor;
    [SerializeField] float period = 2f;
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) return;

        float cycles = Time.time / period; // continually growing over time
        const float tau = Mathf.PI * 2; // going from -1 to 1
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset ;
    }
}
