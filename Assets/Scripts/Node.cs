using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] neighbors;
    private PlayerController player;
    private Enemy enemy;
    public LayerMask obstacles;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
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

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        foreach (Node node in neighbors) {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }
}
