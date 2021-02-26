using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    public float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", destroyTime);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
