using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private PlayerController player;
    private Vector2 destination;
    private bool flipped = false;
    public float speed;
    private Rigidbody2D rb;
    private Transform spriteRoot;
    public Node nearest;
    private List<Node> path;
    public LayerMask obstacles;
    private CircleCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRoot = transform.Find("SpriteRoot");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        path = new List<Node>();
        collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = destination - (Vector2)transform.position;

        Vector2 laser = Vector2.Perpendicular(move);
        laser.Normalize();
        Vector2 sideMove = Vector2.zero;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + collider.offset, laser, 2f, obstacles);
        if (hit) {
            sideMove += -laser * (1f - hit.distance / 2);
        }
        hit = Physics2D.Raycast((Vector2)transform.position + collider.offset, -laser, 2f, obstacles);
        if (hit) {
            sideMove += laser * (1f - hit.distance / 2);
        }

        move += sideMove * move.magnitude;

        flipped = move.x < 0;
        if (move.magnitude > 1f) {
            move.Normalize();
        }

        print(move.magnitude);
        
        rb.velocity = move * speed;
        

        if (flipped) {
            spriteRoot.localScale = new Vector3(-1f,1f,1f);
        } else {
            spriteRoot.localScale = Vector3.one;
        }

        if (nearest != null && player.nearest != null && (path.Count == 0 || path[path.Count-1] != player.nearest)) {
            Search();
            destination = path[0].transform.position;
            path.RemoveAt(0);
        }

        Vector2 displacement = player.transform.position - transform.position;
        if (!Physics2D.Raycast(transform.position, displacement, displacement.magnitude, obstacles)) {
            destination = player.transform.position;
        } else if(move.magnitude < .5f) {
            destination = path[0].transform.position;
            path.RemoveAt(0);
        }
    }

    private void Search() {
        Node start = nearest;
        Heap<Edge> queue = new Heap<Edge>(100);
        Dictionary<Node, Node> predecessors = new Dictionary<Node,Node>();
        Dictionary<Node, float> distances = new Dictionary<Node, float>();
        queue.Insert(new Edge(null, start), 0);
        Node current = start;
        while (queue.size > 0) {
            float distance;
            Edge edge = queue.Pop(out distance);
            current = edge.end;
            Node parent = edge.start;
            if (!distances.ContainsKey(current) || distances[current] > distance) {
                distances[current] = distance;
                predecessors[current] = parent;
            }
            if (current == player.nearest) {
                break;
            }
            foreach (Node node in current.neighbors) {
                Vector2 displacement = current.transform.position - node.transform.position;
                queue.Insert(new Edge(current, node), distance + displacement.magnitude);
            }
        }
        path.Clear();
        while (current != null) {
            path.Add(current);
            current = predecessors[current];
        }
        path.Reverse();
    }

    private struct Edge {
        public Node start;
        public Node end;
        public Edge(Node start, Node end) {
            this.start = start;
            this.end = end;
        }
    }
}
