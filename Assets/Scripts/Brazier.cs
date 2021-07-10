using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Brazier : MonoBehaviour
{
    private Canvas canvas;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.Find("Canvas").GetComponent<Canvas>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && (player.position - transform.position).magnitude < 5f) {

        }
    }

    private IEnumerator ReturnToMenu() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }

    private void EndLevel() {
        canvas.enabled = true;
        StartCoroutine("ReturnToMenu");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            EndLevel();
        }
    }

    
}
