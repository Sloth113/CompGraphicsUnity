using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcess : MonoBehaviour {
    public Material _materail;
    public float _blurStrength = 1; 
    public float _blurWidth = 0.2f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //If opengl this will not be 0-1, will be 0-width(big)
        float ImageWidth = 1;
        float ImageHeight = 1;

        _materail.SetFloat("_BlurStrength", _blurStrength);
        _materail.SetFloat("_BlurWidth", _blurWidth);

        _materail.SetFloat("_imgHeight", ImageWidth);
        _materail.SetFloat("_imgWidth", ImageHeight);

        Graphics.Blit(source, destination, _materail);


    }
}
