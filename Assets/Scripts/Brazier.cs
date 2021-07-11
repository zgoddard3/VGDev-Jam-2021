using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Brazier : MonoBehaviour
{
    private Canvas canvas;
    private Transform player;
    private Lantern lantern;
    private GameObject flame;
    public Sprite litSprite;
    private AudioSource audioSource;
    public string nextLevel;
    public static bool lit;
    public Image blackout;
    public Vector3 teleport;
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.Find("Canvas").GetComponent<Canvas>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lantern = GetComponent<Lantern>();
        flame = transform.Find("Flame").gameObject;
        flame.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        lit = false;
        blackout.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && (player.position - transform.position).magnitude < 5f) {
            player.GetComponent<PlayerController>().lantern.burnRate = 0;
            lantern.fuel = lantern.maxFuel;
            lit = true;
            flame.SetActive(true);
            audioSource.Play();
            GetComponent<SpriteRenderer>().sprite = litSprite;
            EndLevel();
            GameObject go = GameObject.FindGameObjectWithTag("Enemy");
            if (go != null) {
                go.transform.position = teleport;
            }
        }
    }

    private IEnumerator GoToNextLevel() {
        yield return new WaitForSeconds(2f);
        yield return FadeOut();
        SceneManager.LoadScene(nextLevel);
    }

    private IEnumerator FadeOut() {
        float dt = 1f/30;
        float n = 60;
        for (int i = 0; i < n; i++) {
            blackout.color = Color.Lerp(Color.clear, Color.black, (float)i/n);
            yield return new WaitForSeconds(dt);
        }
    }

    private void EndLevel() {
        canvas.enabled = true;
        StartCoroutine("GoToNextLevel");
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(teleport, .5f);
    }
}
