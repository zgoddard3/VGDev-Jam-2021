using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class Oil : MonoBehaviour
{
    public float fuel;
    //private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            //audioSource.Play();
            //print("attempted to play\n");
            other.GetComponent<PlayerController>().lantern.AddFuel(fuel);
            Destroy(this.gameObject);
            
        }
    }
}
