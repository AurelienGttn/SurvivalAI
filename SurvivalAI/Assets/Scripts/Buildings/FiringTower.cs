using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringTower : MonoBehaviour {

    private Transform target;
    public float range = 15f;

    [Header("Attributes")]
    public float fireRate = 1f;
    private float fireCountDown = 0f;

    public GameObject firingStone;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update()
    {
        if(this.GetComponent<PlaceableBuilding>().isConstructed == true)
        {
            if (fireCountDown <= 0f)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }

            fireCountDown -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(firingStone, transform.position, transform.rotation);
        FiredStone bullet = bulletGO.GetComponent<FiredStone>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
            else
            {
                target = null;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
