using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnd : MonoBehaviour
{
    public GameObject platform;
    public float turretTime;
    public Turret turret;
    private void OnEnable()
    {
       StartCoroutine(Disable());
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(turretTime);
        turret.active = false;
       /* this.gameObject.SetActive(false);
        platform.SetActive(true);*/
    }
}
