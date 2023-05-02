using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spawner : MonoBehaviour
{
    public TextMeshProUGUI WaveText;

    [SerializeField] Transform SpawnerTop;

    [SerializeField] Transform SpawnerL1;
    [SerializeField] Transform SpawnerL2;
    [SerializeField] Transform SpawnerL3;

    [SerializeField] Transform SpawnerR1;
    [SerializeField] Transform SpawnerR2;
    [SerializeField] Transform SpawnerR3;

    [SerializeField] Transform SpawnerD1;
    [SerializeField] Transform SpawnerD2;

    public GameObject seeker;
    public GameObject runner;
    public GameObject stalker;
    public GameObject bomba;
    public GameObject Ghost;
    public GameObject Omega;

    [SerializeField] float wave = 0;
    [SerializeField] bool inwave = false;

    public Transform target;
    private Vector2 velocity = Vector2.up;
    private float seekerWave = 1;
    private float runnerWave = 0;
    private float stalkerWave = 0;
    private float bombaWave = 0;
    private float GhostWave = 0;

    public GameObject BloodOrb;
    private float Bloods;

    // Start is called before the first frame update
    void Start()
    {
        WaveText.text = "Wave" + wave.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        WaveText.text = "Wave " + wave.ToString();
        if(inwave == true)
        {
            gameObject.transform.localPosition = Vector2.SmoothDamp(gameObject.transform.localPosition, new Vector2(2.16f, 30),ref velocity,3);
        }
        if(inwave == false)
        {
            gameObject.transform.localPosition = Vector2.SmoothDamp(gameObject.transform.localPosition, new Vector2(2.16f, 12), ref velocity,2);
        }
        if (GameObject.Find ("seekers(Clone)") == null && GameObject.Find ("runner(Clone)") == null && GameObject.Find ("stalker(Clone)") == null && GameObject.Find ("bomba(Clone)") == null && GameObject.Find ("Omega(Clone)") == null)
        {
;
            inwave = false;
            Physics2D.IgnoreLayerCollision(8,13,false);
            Physics2D.IgnoreLayerCollision(9,13,false);
            Physics2D.IgnoreLayerCollision(12,13,false);
            Physics2D.IgnoreLayerCollision(14,13,false);
        }

        if(wave>9)
        {
            seekerWave = wave - 8;
        }
        if(wave>=4)
        {
            runnerWave = wave - 4;
        }
        if(wave>10)
        {
            stalkerWave = wave - 9;
        }
        if(wave>6)
        {
            bombaWave = wave -5;
        }
        if(wave>7)
        {
            GhostWave = wave -6;
        }


    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "bullet")
        {
            inwave = true;
            wave += 1;
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
            SpawnRunner();
            SpawnStalker();
            Spawnbomba();
            SpawnGhost();
        }
    }
    void SpawnSeeker()
    {
        for(int i = 0 ; i<=seekerWave/2 ;i++)
        {
            if(wave >= 1)
            {
                Instantiate(seeker,SpawnerL1.position,Quaternion.identity);
                Instantiate(seeker,SpawnerR1.position,Quaternion.identity);
            }
        }
        for(int i = 0 ; i<=seekerWave/2 ;i++)
        {
            if(wave >= 3)
            {
                Instantiate(seeker,SpawnerL3.position,Quaternion.identity);      
                Instantiate(seeker,SpawnerR3.position,Quaternion.identity);     
            }
        }   
        for(int i = 0 ; i<=wave/2;i++)
        { 
            if(wave >= 7)
            {
                Instantiate(seeker,SpawnerD1.position,Quaternion.identity);
                Instantiate(seeker,SpawnerD2.position,Quaternion.identity);  
            }
            if(wave >= 9)
            {
                Instantiate(seeker,SpawnerTop.position,Quaternion.identity); 
            }
        }
    }
    void SpawnRunner()
    {
        for(int i =0 ; i<=runnerWave/2;i++)
        {
            if(wave >= 2)
            {
            Instantiate(runner,SpawnerL2.position,Quaternion.identity);
            }
        }
        for(int i =0 ; i<=runnerWave/2;i++)
        {
            if(wave>= 4)
            {
                Instantiate(runner,SpawnerR2.position,Quaternion.identity);
            }
        }
    }
    void SpawnStalker()
    {
        
        if(wave>=6)
        {
            Instantiate(stalker,SpawnerL2.position,Quaternion.identity);
        }
        if(wave>= 8)
        {
            Instantiate(stalker,SpawnerR2.position,Quaternion.identity);
        }
        for(int i =0 ; i<=stalkerWave/2;i++)
        {
            if(wave>= 10)
            {
                Instantiate(stalker,SpawnerTop.position,Quaternion.identity);            
            }
        }
    }
    void Spawnbomba()
    {
        for(int i =0 ; i<=bombaWave/2;i++)
        {
            if(wave >= 5)
            {
            Instantiate(bomba,SpawnerL2.position,Quaternion.identity);
            }
        }
        for(int i =0 ; i<=bombaWave/2 - 1;i++)
        {
            if(wave>= 6)
            {
                Instantiate(bomba,SpawnerR2.position,Quaternion.identity);
            }
        }
    }
        void SpawnGhost()
    {
        for(int i =0 ; i<=GhostWave/2;i++)
        {
            if(wave >= 4)
            {
            Instantiate(Ghost,SpawnerL2.position,Quaternion.identity);
            }
        }
        for(int i =0 ; i<=GhostWave/2;i++)
        {
            if(wave>= 7)
            {
                Instantiate(Ghost,SpawnerR2.position,Quaternion.identity);
            }
        }
    }

    void SpawnBlood()
    {
        GameObject newObject = Instantiate(BloodOrb,transform.position,Quaternion.identity);
    }
}
