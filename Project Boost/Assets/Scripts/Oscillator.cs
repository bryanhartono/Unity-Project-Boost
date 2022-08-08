using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 start_position;
    [SerializeField] Vector3 movement_vector;
    float movement_factor;
    [SerializeField] float period = 10f;

    // Start is called before the first frame update
    void Start()
    {
        start_position = transform.position;
        Debug.Log(start_position);
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) // Mathf.Episilon -> smallest float value
        {
            return;
        }

        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2; // constant value of 6.283
        float raw_sin_wave = Mathf.Sin(cycles * tau); // value is from -1 to 1

        movement_factor = (raw_sin_wave + 1f) / 2f; // value is from 0 to 1;
        Vector3 offset = movement_vector * movement_factor;
        transform.position = start_position + offset;
    }
}
