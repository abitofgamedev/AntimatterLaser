using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeShip : MonoBehaviour
{
    public List<Rigidbody> rigidbodies;
    public float Force;
    public float Radius;

    public float Drag;
    public float AngularDrag;

    public void Explode(Vector3 pos,Vector3 source) 
    { 
        foreach(var i in rigidbodies)
        {
            i.drag = Drag;
            i.angularDrag = AngularDrag;
            //i.AddForceAtPosition((i.transform.position - source).normalized*Force, pos, ForceMode.Impulse);
            i.AddExplosionForce(Force,pos,Radius);
            i.angularVelocity = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)) * Vector3.forward;
        }
    }
}
