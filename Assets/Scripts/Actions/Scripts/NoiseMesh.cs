using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Actions/NoiseMesh")]
public class NoiseMesh : Action {
    public int detail = 100;
    public override void Apply(GameObject go)
    {
        MeshFilter filter = go.GetComponent<MeshFilter>();
        Mesh mesh = filter.mesh;
        MeshCollider collider = go.GetComponent<MeshCollider>();



        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = mesh.uv;
        Vector3[] normals = mesh.normals;

        float[][][] field = NoiseGenerator.Perlin3D(100, 100, 100, new Vector3(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3)), 10);
        
        for(int i = 0; i < vertices.Length; i++)
        {
            //vertices[i] += new Vector3(field[(int)vertices[i].x][(int)vertices[i].y][(int)vertices[i].z], field[(int)vertices[i].x][(int)vertices[i].y][(int)vertices[i].z], field[(int)vertices[i].x][(int)vertices[i].y][(int)vertices[i].z]);
            // take the normal by comparing neighbouring points
            //normals[index00] = new Vector3(getH(Mathf.Max(i - 1, 0), j) - getH(Mathf.Min(i + 1, span), j), 1, getH(i, Mathf.Max(j - 1, 0)) - getH(i, Mathf.Min(j + 1, span)));
            //uvs[index00] = new Vector2(i / (1.0f * span), j / (1.0f * span));

        }
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.normals = normals;


        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        
        collider.sharedMesh = mesh;
        
        filter.mesh = mesh;      
    }
}
