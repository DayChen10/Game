using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    [SerializeField] Transform Spawnertut;

    public GameObject enemy;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            for(int i = 0;i<=3;i++)
            {
            Instantiate(enemy,Spawnertut.position,Quaternion.identity);
            }
        }
    }
}
