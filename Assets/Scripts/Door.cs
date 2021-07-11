using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static bool hasKey;
    private Transform player;
    private Animator animator;
    public Vector3 spawn;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        hasKey = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && hasKey && (player.position - transform.position).magnitude < 5f) {
            animator.SetTrigger("Open");
            StartCoroutine("WaitAndDestroy");
        }
    }

    private IEnumerator WaitAndDestroy() {
        yield return new WaitForSeconds(1f);
        GameObject.Instantiate(enemy, spawn, transform.rotation);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawn, .5f);
    }
}
