using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaster : MonoBehaviour
{
    private List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    private bool lockFlash = false;

    // Start is called before the first frame update
    void Start()
    {
        RecursiveSearch(transform);
    }

    // Update is called once per frame
    void Update()
    {
        SpriteSort();
    }

    private void RecursiveSearch(Transform current) {
        SpriteRenderer sprite = current.GetComponent<SpriteRenderer>();
        if (sprite) {
            sprites.Add(sprite);
        }
        for (int i=0; i < current.childCount; i++) {
            RecursiveSearch(current.GetChild(i));
        }
    }

    private void SetColor(Color color) {
        foreach (SpriteRenderer sprite in sprites) {
            if (sprite.gameObject.name.StartsWith("Decal")) {
                continue;
            }
            sprite.color = color;
        }
    }

    public void StartFlash() {
        if (lockFlash) {
            return;
        }
        lockFlash = true;
        StartCoroutine(Flash(.4f, Color.red, 1));
    }

    public IEnumerator Flash(float duration, Color color, int n) {
        float dt = 1f/30;
        for (int i=0; i < n; i++) {
            float counter = 0f;
            while (counter < duration) {
                counter += dt;
                SetColor(Color.Lerp(Color.white, color, Mathf.Cos(Mathf.PI/2*(2*counter-duration)/duration)));
                yield return new WaitForSeconds(dt);
            }
        }
        lockFlash = false;
    }

    public void SpriteSort() {
        foreach(SpriteRenderer sprite in sprites) {
            sprite.sortingOrder = (int)(transform.position.y * -100);
        }
    }
}
