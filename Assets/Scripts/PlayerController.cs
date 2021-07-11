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
    private AudioSource audioSource;
    public AudioClip[] footsteps;
    private int footstepIndex = 0;
    public GameObject bug;
    private Vector3 bugSpawn;
    private int bugCount = 0;
    public Node nearest;
    public AudioClip deathClip;
    private bool dead = false;
    private AudioSource[] allAudioSources;
    public string currentScene;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRoot = transform.Find("Sprites");
        animator = GetComponent<Animator>();
        canvas = transform.Find("Canvas").GetComponent<Canvas>();
        audioSource = GetComponent<AudioSource>();
        bugSpawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead) {
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (move.magnitude > 1f)
            {
                move.Normalize();
            }

            rb.velocity = move * speed;

            if (Mathf.Abs(move.x) > 0)
            {
                flipped = move.x < 0;
            }
            if (flipped)
            {
                spriteRoot.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                spriteRoot.localScale = Vector3.one;
            }

            animator.SetFloat("Speed", speed * move.magnitude);

            if (lantern.fuel <= 0f)
            {
                Die();
            }
            if ((transform.position - bugSpawn).magnitude > 5f && bugCount < 50)
            {
                GameObject.Instantiate(bug, bugSpawn, transform.rotation);
                bugSpawn = transform.position;
                bugCount += 1;
            }
        }
    }

    void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    private void Die()
    {
        StopAllAudio();
        dead = true;
        audioSource.clip = deathClip;
        audioSource.Play();
        canvas.enabled = true;
        StartCoroutine("RestartScene");
    }

    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(currentScene);
    }

    public void PlayFootstep()
    {
        audioSource.clip = footsteps[footstepIndex];
        footstepIndex += 1;
        if (footstepIndex >= footsteps.Length)
        {
            footstepIndex = 0;
        }
        audioSource.Play();
    }
}