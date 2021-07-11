using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    public PlayerController player;
    public float speed;
    private Vector2 offset;
    private float counter;
    private bool chasing = false;
    private Rigidbody2D rb;
    private bool flipped = false;
    private Transform body;
    private AudioSource audioSource;
    [Range(0f,1f)]
    public float maxVolume = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        body = transform.Find("Body");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 displacement = player.transform.position - transform.position;
        audioSource.volume = Mathf.Max(0, 1 - displacement.magnitude/10) * maxVolume;
        if (displacement.magnitude < 10) {
            chasing = true;
            
        }

        if (chasing) {
            flipped = displacement.x < 0;
            displacement += offset;
            Vector2 move;
            if (!Brazier.lit) {
                move = displacement.normalized * (-Mathf.Max(player.lantern.fuel/2, 2f) + displacement.magnitude);
            } else {
                move = -displacement.normalized;
            }
            
            if (move.magnitude > 1f) {
                move.Normalize();
            }
            rb.velocity = move * speed;
            if (flipped) {
                body.localScale = new Vector3(-1f,1f,1f);
            } else {
                body.localScale = Vector3.one;
            }
        }

        counter -= Time.deltaTime;
        if (counter < 0) {
            counter = .5f;
            offset = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }
}
