using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    public float fuel;
    public float maxFuel = 5f;
    public float burnRate;
    private Transform lightTransform;
    

    // Start is called before the first frame update
    void Start()
    {
        fuel = maxFuel;
        lightTransform = transform.Find("Light");
    }

    // Update is called once per frame
    void Update()
    {
        fuel -= burnRate * Time.deltaTime;
        if (fuel < 0) {
            fuel = 0;
        }
        lightTransform.localScale = Vector3.one * (fuel + Mathf.Sin(10*Time.time) * fuel / 50f);
    }

    public void AddFuel(float amount) {
        fuel += amount;
        if (fuel < maxFuel) {
            fuel = maxFuel;
        }
    }
}
