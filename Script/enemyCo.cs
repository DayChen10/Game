using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCo : MonoBehaviour
{
    [SerializeField] float BloodNum = 1;
    public GameObject objectToSpawn;
    [SerializeField] float health = 1f;
    private float i = 0;
    [SerializeField] Rigidbody2D enemy;

    void Start()
    {
        enemy.velocity = new Vector2(Random.Range(0f,10f),Random.Range(0f,10f));
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            health--;
        }
        if (health <= 0)
        {
            SpawnObject();
            UnityEngine.Object.Destroy(gameObject);
        }      
    }
    
    void SpawnObject()
    {
        while(i<=BloodNum + Random.Range(-1f,1f))
        {
            SpawnOrb();
            i += 1;
        }
            
    }
    void SpawnOrb()
    {
        GameObject newObject = Instantiate(objectToSpawn,transform.position,Quaternion.identity);
    }

}
