using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BloodOrb : MonoBehaviour
{
    [SerializeField] Rigidbody2D Orb;
    void Start()
    {
        Orb.velocity = new Vector2(Random.Range(0f,1f),Random.Range(0f,1f));
    }
    [SerializeField] float livetime = 5;
    void Update() 
    {
        Destroy(gameObject,livetime);
    }
}


