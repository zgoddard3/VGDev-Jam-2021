using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    public float fuel;
    public float maxFuel = 5f;
    public float burnRate;
    private Transform lightTransform;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        lightTransform = transform.Find("Light");
        audioSource = GetComponent<AudioSource>();
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
        audioSource.Play();
        if (fuel > maxFuel) {
            fuel = maxFuel;
        }
    }
}
