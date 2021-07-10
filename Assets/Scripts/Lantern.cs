using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lantern : MonoBehaviour
{
    public float fuel;
    public float maxFuel = 5f;
    public float burnRate;
    private Transform lightTransform;
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        fuel = maxFuel;
        lightTransform = transform.Find("Light");
        canvas = transform.Find("Canvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        fuel -= burnRate * Time.deltaTime;
        if (fuel <= 0f) {
            fuel = 0;
            Die();
        }
        lightTransform.localScale = Vector3.one * (fuel + Mathf.Sin(10*Time.time) * fuel / 50f);
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
