using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Deforms the mesh from a list of hit points 
/// </summary>
public class MeshDeform : MonoBehaviour {
    public List<Vector3> _hitPoints;
    public bool _hit;
    public Vector3 _pos;
    public GameObject _test;
    public float _pointSize = 0.1f;
    public float _deformAmount = 0.1f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    _pos = hit.point;
                    Hit(_pos);
                }

                if (_hit)
                {
                    _hit = false;
                    Hit(_pos);
                }
            }
        }

	}

    private void Hit(Vector3 pos)
    {
        _hitPoints.Add(pos);
        CalcShape();
    }

    private void CalcShape()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        Mesh mesh = filter.mesh;
        MeshCollider collider = GetComponent<MeshCollider>();



        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = mesh.uv;
        Vector3[] normals = mesh.normals;

        float minX = vertices[0].x; float minY = vertices[0].y; float minZ = vertices[0].z; float maxX = vertices[0].x; float maxY = vertices[0].y; float maxZ = vertices[0].z;

        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i].x < minX) minX = vertices[i].x;
            if (vertices[i].y < minY) minY = vertices[i].y;
            if (vertices[i].z < minZ) minZ = vertices[i].z;
            if (vertices[i].x > maxX) maxX = vertices[i].x;
            if (vertices[i].y > maxY) maxY = vertices[i].y;
            if (vertices[i].z > maxZ) maxZ = vertices[i].z;

        }
        //Debug.Log(minX + "-" + maxX + "," + minY + "-" + maxY + "," + minZ + "-" + maxZ );

        float xRange = maxX - minX;
        float yRange = maxY - minY;
        float zRange = maxZ - minZ;

       // Debug.Log(Vector3.Magnitude((new Vector3(vertices[0].x * transform.localScale.x, vertices[0].y * transform.localScale.y, vertices[0].z * transform.localScale.z)) + transform.position - _hitPoints[0]));

        for (int j = 0; j < _hitPoints.Count; j++) {
            for (int i = 0; i < vertices.Length; i++)
            {
                if (Vector3.Magnitude((new Vector3(vertices[i].x * transform.localScale.x, vertices[i].y * transform.localScale.y, vertices[i].z * transform.localScale.z)) + transform.position - _hitPoints[j]) < _pointSize)
                {
                   // Debug.Log("Vert" + _hitPoints[j]);
                    Vector3 dis = new Vector3();
                    dis = (transform.position - _hitPoints[j]).normalized * _deformAmount;
                    vertices[i] += dis;
                }

            }
        }
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.normals = normals;


        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        collider.sharedMesh = mesh;

        filter.mesh = mesh;
        _hitPoints.Clear();
    }
}
