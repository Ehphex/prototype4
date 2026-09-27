using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRB;
    private GameObject player;
    public float speed = 3.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRB.AddForce(lookDirection * speed);
        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
