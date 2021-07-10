using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private Vector2 destination;
    private bool flipped = false;
    public float speed;
    private Rigidbody2D rb;
    private Transform spriteRoot;
    public Node nearest;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRoot = transform.Find("SpriteRoot");
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = player.transform.position - transform.position;

        flipped = move.x < 0;
        if (move.magnitude > 1f) {
            move.Normalize();
        }
        rb.velocity = move * speed;
        if (flipped) {
            spriteRoot.localScale = new Vector3(-1f,1f,1f);
        } else {
            spriteRoot.localScale = Vector3.one;
        }
    }

    private void Search() {
        Node start = nearest;
        
    }
}
