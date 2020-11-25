using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public Camera cam;

    public float moveSpeed = 5f;

    const string stateShooting = "Shooting";

    Animator anim;
    private PlayerController player;

    Vector2 movement;
    Vector2 mousePos;

    

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool(stateShooting, false);
                
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);


        anim.SetBool(stateShooting, Shooting());
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
    }

    bool Shooting()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
