using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Node : MonoBehaviour
{
    public List<Node> neighbors;
    private PlayerController player;
    private Enemy enemy;
    public LayerMask obstacles;
    private static bool generated = false;
    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        if (!generated) {
            print("Generating");
            Generate();
            generated = true;
            SceneManager.sceneLoaded += delegate{generated = false;};
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 displacement;
        float minDistance;
        if (player.nearest == null) {
            player.nearest = this;
        }
        if (player.nearest != this) {
            displacement = player.transform.position - player.nearest.transform.position;
            minDistance = displacement.magnitude;
            displacement = player.transform.position - transform.position;
            if (minDistance > displacement.magnitude) {
                if (!Physics2D.Raycast(transform.position, displacement, displacement.magnitude, obstacles)) {
                    player.nearest = this;
                }
            }
        }

        if (enemy.nearest == null) {
            enemy.nearest = this;
        }
        if (enemy.nearest != this) {
            displacement = enemy.transform.position - enemy.nearest.transform.position;
            minDistance = displacement.magnitude;
            displacement = enemy.transform.position - transform.position;
            if (minDistance > displacement.magnitude) {
                if (!Physics2D.Raycast(transform.position, displacement, displacement.magnitude, obstacles)) {
                    enemy.nearest = this;
                }
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, .5f);
        if (neighbors == null) {
            return;
        }
        Gizmos.color = Color.red;
        foreach (Node node in neighbors) {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }
    
    public void Generate() {
        Transform graph = transform.parent;
        if (graph == null) {
            return;
        }

        for (int i = 0; i < graph.childCount; i++) {
            Node node1 = graph.GetChild(i).GetComponent<Node>();
            if (node1 == null) {
                continue;
            }
            node1.neighbors.Clear();
            for (int j = 0; j < graph.childCount; j++) {
                Node node2 = graph.GetChild(j).GetComponent<Node>();
                if (i == j || node2 == null) {
                    continue;
                }
                Vector2 displacement = node2.transform.position - node1.transform.position;
                if (!Physics2D.Raycast(node1.transform.position, displacement, displacement.magnitude, node1.obstacles)) {
                    node1.neighbors.Add(node2);
                }
            }
        }
    }
}
