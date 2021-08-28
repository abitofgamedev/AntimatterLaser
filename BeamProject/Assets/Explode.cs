using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float Health;

    public GameObject ExplosionPrefab;
    public ParticleSystem ExplosionEffect;
    bool destroy;

    public void ReduceHealth(float value, Vector3 pos, Vector3 source)
    {
        if (destroy)
        {
            return;
        }
        Health -= value;
        if (Health <= 0)
        {
            GameObject explosionPrefab = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            explosionPrefab.GetComponent<ExplodeShip>().Explode(pos, source);
            Instantiate(ExplosionEffect, pos, Quaternion.identity);
            Destroy(gameObject);
            destroy = true;
        }
    }
}
