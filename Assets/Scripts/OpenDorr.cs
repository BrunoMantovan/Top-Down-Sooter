using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDorr : MonoBehaviour
{
    public Vector3 moveDistance;
   
    public void OpenDoor()
    {
        this.transform.position += moveDistance;
    }


}
