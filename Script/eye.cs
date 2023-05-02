using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eye : MonoBehaviour
{
    public Vector3 PlayerPos;
    public GameObject tar;
    [SerializeField] Transform aimTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tar = GameObject.FindWithTag("Player");
        aimTarget = tar.transform;
        PlayerPos = aimTarget.position;
        Vector3 lookDir = PlayerPos-transform.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x)* Mathf.Rad2Deg-90f;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
    }
}
