using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Brazier : MonoBehaviour
{
    private Canvas canvas;
    private Transform player;
    private Lantern lantern;
    private GameObject flame;
    public Sprite litSprite;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.Find("Canvas").GetComponent<Canvas>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lantern = GetComponent<Lantern>();
        flame = transform.Find("Flame").gameObject;
        flame.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && (player.position - transform.position).magnitude < 5f) {
            lantern.fuel = lantern.maxFuel;
            flame.SetActive(true);
            audioSource.Play();
            GetComponent<SpriteRenderer>().sprite = litSprite;
            EndLevel();
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
}
