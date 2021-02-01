using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitProjectile : MonoBehaviour
{
    public float speed;

    public Transform playerPos;
    public Vector2 target;

    public int projectileDamage = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(playerPos.position.x, playerPos.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if(player != null)
        {
            player.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "bulletColl")
        {
            Destroy(gameObject);
        }
    }
}
