using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] float BloodNum = 1;
    public GameObject objectToSpawn;
    [SerializeField] float health = 1f;
    private float i = 0;
    [SerializeField] Rigidbody2D enemy;

    private Vector2 velocity = Vector2.zero;
    public Transform target;
    public GameObject tar;
    [SerializeField] Rigidbody2D ghost;

    void Start()
    {
        ghost = GetComponent<Rigidbody2D>();
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
            SpawnOrbs();
            i += 1;
        }
            
    }
    void SpawnOrbs()
    {
        GameObject newObject = Instantiate(objectToSpawn,transform.position,Quaternion.identity);
    }

    void Update() 
    {
        tar = GameObject.FindWithTag("Player");
        target = tar.transform;
        ghost.velocity = new Vector2(0,0);
        gameObject.transform.localPosition = Vector2.SmoothDamp(gameObject.transform.localPosition, new Vector2(tar.transform.position.x, tar.transform.position.y), ref velocity,1f);
    }

}
