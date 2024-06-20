using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeCamera : MonoBehaviour
{
    [SerializeField] private GameObject bike;
    [SerializeField] private Vector3 offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = bike.transform.position + offset;
        transform.rotation = bike.transform.rotation;
    }
}
