using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePush : MonoBehaviour {
    private List<Rigidbody> _objs;
    public float _interval = 1;
    public Vector3 _force = new Vector3(0,1,0);
    public ParticleSystem _particles;
    private void Start()
    {
        _objs = new List<Rigidbody>();
    }
    private void FixedUpdate()
    {
        foreach (Rigidbody r in _objs)
        {
            r.AddForce(_force );
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody oRig = other.GetComponent<Rigidbody>();
        _objs.Add(oRig);
    }
    private void OnTriggerExit(Collider other)
    {
        Rigidbody oRig = other.GetComponent<Rigidbody>();
        _objs.Remove(oRig);
    }
    private void OnEnable()
    {
        _particles.Play();
    }
    private void OnDisable()
    {
        _particles.Stop();
    }
    /*
    private void OnTriggerStay(Collider other)
    {
        Rigidbody oRig = other.GetComponent<Rigidbody>();
        if (oRig != null)
        {

            oRig.AddForce(_force );
            //Particle effect
                //Create
                //Add to obj
        }

    }
    */
}
