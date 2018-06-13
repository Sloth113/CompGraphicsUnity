using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeform : MonoBehaviour {
    public List<Vector3> _hitPoints;
    public bool _hit;
    public Vector3 _pos;
    public GameObject _test;
	// Use this for initialization
	void Start () {
        CalcShape();
	}
	
	// Update is called once per frame
	void Update () {
        if (_hit)
        {
            Hit(_pos);
            _hit = false;
        }

	}

    private void Hit(Vector3 pos)
    {
        CalcShape();
    }

    private void CalcShape()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        

        foreach(Vector3 v in mesh.vertices)
        {
            //Instantiate<GameObject>(_test, v + new Vector3(transform.position.x * transform.localScale.x, transform.position.y * transform.localScale.y, transform.position.z * transform.localScale.z), transform.rotation);
        }
    }
}
