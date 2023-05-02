using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCore : MonoBehaviour
{
    
    private Transform aimTransform;
    public Camera cam;
    Vector3 mousePos;
    private void Awake() 
    {
        aimTransform = transform.Find("Aim");
    }
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 lookDir = mousePos-transform.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x)* Mathf.Rad2Deg-90f;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
    }
}
