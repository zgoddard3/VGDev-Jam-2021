using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private Transform spriteRoot;
    private bool flipped;
    private Animator animator;
    public Lantern lantern;
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRoot = transform.Find("Sprites");
        animator = GetComponent<Animator>();
        canvas = transform.Find("Canvas").GetComponent<Canvas>();
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
        
        animator.SetFloat("Speed", speed * move.magnitude);

        if (lantern.fuel <= 0f) {
            Die();
        }
    }

    private void Die() {
        canvas.enabled = true;
        StartCoroutine("ReturnToMenu");
    }

    private IEnumerator ReturnToMenu() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }
}
