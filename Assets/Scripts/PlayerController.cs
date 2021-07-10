using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private Transform spriteRoot;
    private bool flipped;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRoot = transform.Find("Sprites");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (move.magnitude > 1f) {
            move.Normalize();
        }
        
        rb.velocity = move * speed;

        if (Mathf.Abs(move.x) > 0) {
            flipped = move.x < 0;
        }
        if (flipped) {
            spriteRoot.localScale = new Vector3(-1f,1f,1f);
        } else {
            spriteRoot.localScale = Vector3.one;
        }
        
    }
}
