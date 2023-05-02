using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tuteyetrigger : MonoBehaviour
{
    private Vector2 velocity = Vector2.up;

    [SerializeField] Transform SpawnerT1;
    [SerializeField] Transform SpawnerT2;

    [SerializeField] bool inwave = false;

    public GameObject BloodOrb;
    private float Bloods;

    public GameObject seeker;

    void Update()
    {
        if(inwave == true)
        {
            gameObject.transform.localPosition = Vector2.SmoothDamp(gameObject.transform.localPosition, new Vector2(-47.4f, 43.55f),ref velocity,3);
        }
        if(inwave == false)
        {
            gameObject.transform.localPosition = Vector2.SmoothDamp(gameObject.transform.localPosition, new Vector2(-47.4f, 31.51f), ref velocity,2);
        }
        if (GameObject.Find ("seekers(Clone)") == null)
        {
;
            inwave = false;
            Physics2D.IgnoreLayerCollision(8,13,false);
            Physics2D.IgnoreLayerCollision(9,13,false);
            Physics2D.IgnoreLayerCollision(12,13,false);
            Physics2D.IgnoreLayerCollision(14,13,false);
        }
    }
        void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "bullet")
        {
            inwave = true;
            Physics2D.IgnoreLayerCollision(8,13,true);
            Physics2D.IgnoreLayerCollision(9,13,true);
            Physics2D.IgnoreLayerCollision(12,13,true);
            Physics2D.IgnoreLayerCollision(14,13,true);

            Bloods = Random.Range(5f,30f);
            for(int i = 0;i<=Bloods;i++)
            {
                SpawnBlood();
            }

            SpawnSeeker();
        }
    }
    void SpawnSeeker()
    {
        for(int i = 0;i<=1;i++)
        {
        Instantiate(seeker,SpawnerT1.position,Quaternion.identity);
        Instantiate(seeker,SpawnerT2.position,Quaternion.identity);
        }
    }

    void SpawnBlood()
    {
        GameObject newObject = Instantiate(BloodOrb,transform.position,Quaternion.identity);
    }    
    
}
