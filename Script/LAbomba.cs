using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAbomba : MonoBehaviour
{
    [SerializeField] float BloodNum = 1;
    public GameObject objectToSpawn;
    public GameObject Bomb;
    public GameObject Burst;
    public GameObject Core;
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
            StartCoroutine(BlowUp());
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
            Debug.Log("B");
            StartCoroutine(BlowUp());
            Core.transform.GetComponent<Renderer>().material.color = new Color(250,0,0);
            for(int i = 0 ; i<1 ; i++)
            {
                Instantiate(Bomb,transform.position,Quaternion.identity);
                Instantiate(Burst,transform.position,Quaternion.identity);
            }
        }
            
    }
    void SpawnOrb()
    {
        GameObject newObject = Instantiate(objectToSpawn,transform.position,Quaternion.identity);
    }
    private IEnumerator BlowUp()
    {
        yield return new WaitForSeconds(1.5f);
    }

}
