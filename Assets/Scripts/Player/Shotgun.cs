using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public int pelletAmount;
    public float spread;
    public float pelletForce = 60f;
    public GameObject pellet;
    public Transform shotgunFireTip;
    List<Quaternion> pellets;
    
    void Awake()
    {
        pellets = new List<Quaternion>(pelletAmount);
        for (int i = 0; i < pelletAmount; i++)
        {
            pellets.Add(Quaternion.Euler(Vector2.zero));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot4();
        }
    }

    private void Shoot4()
    {

        for (int i = 0; i < pelletAmount; i++)
        {
            pellets[i] = Random.rotation;
            GameObject p = Instantiate(pellet, shotgunFireTip.position, shotgunFireTip.rotation);
            p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spread);
            p.GetComponent<Rigidbody2D>().AddForce(p.transform.right * pelletForce);
            i++;
        }
    }
}
