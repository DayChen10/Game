using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject BurstRing;

    public float bulletForce = 20f;
    float lastfired;
    [SerializeField] float FireRate = 5;

    public Image BurstUI;

    void Update() 
    {
        if(Input.GetButton("Fire1"))
        {
            if (Time.time - lastfired > 1 / FireRate)
            {
                lastfired = Time.time;
                Shoot();
                BurstUI.fillAmount += 0.01f;

            }
        }
        if(Input.GetButton("burst")&&BurstUI.fillAmount>=1)
        {
            Burst();  
            BurstUI.fillAmount = 0f;
        }
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab,firePoint.position,firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up*bulletForce,ForceMode2D.Impulse);
    }
    void Burst()
    {
        Instantiate(BurstRing,transform.position,Quaternion.identity);            
    }
}
