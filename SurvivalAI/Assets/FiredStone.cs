using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredStone : MonoBehaviour {

    private Transform target;

    public float speed = 10f;
    public float damage = 10f;
    //public GameObject impactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        target.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
