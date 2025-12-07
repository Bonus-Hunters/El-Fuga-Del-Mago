using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera cam;
    private Vector3 destination;
    public GameObject projectile;
    public Transform firePoint;
    public float projectileSpeed = 30f;
    private float timeToFire;
    public float fireRate = 4;
    public float arcRange = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && Time.time >= timeToFire)//right click
        {
            timeToFire = Time.time + 1 /fireRate;
            ShootProjectile();
        }
    }
    void ShootProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else destination = ray.GetPoint(1000);
        InstantiateProjectile();
    }
    void InstantiateProjectile()
    {
        //instantiate the projectile
        var projectileObj  = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        //direction and speed
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
        //animate
        iTween.PunchPosition(projectileObj, 
            new Vector3(Random.Range(arcRange,arcRange), Random.Range(arcRange, arcRange),0)
            ,Random.Range(0.5f,2));
    }
}
