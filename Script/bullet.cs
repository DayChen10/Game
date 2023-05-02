using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{ 
    public GameObject hitEffect;

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect,transform.position,Quaternion.identity);
        UnityEngine.Object.Destroy(effect,0.2f);
        UnityEngine.Object.Destroy(gameObject);
    }
}
