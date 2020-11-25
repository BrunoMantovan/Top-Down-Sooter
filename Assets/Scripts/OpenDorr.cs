using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDorr : MonoBehaviour
{
   
    public void OpenDoor()
    {
        this.transform.position += new Vector3(8, 0, 0);
    }


}
