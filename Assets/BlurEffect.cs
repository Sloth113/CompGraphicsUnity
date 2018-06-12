using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurEffect : MonoBehaviour {
    public Material _materail;
    public float _blurStrength = 1;
    private float _blurCurrent = 0;
    public float _blurWidth = 0.2f;
    public float _blurRecovery = 3.0f;
    private float _blurRecTimer = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        _blurCurrent = Mathf.Lerp(_blurStrength, 0, _blurRecTimer / _blurRecovery);
        if(_blurRecTimer < _blurRecovery)
        {
            _blurRecTimer += Time.deltaTime;
        }
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //If opengl this will not be 0-1, will be 0-width(big)
        float ImageWidth = 1;
        float ImageHeight = 1;

        _materail.SetFloat("_BlurStrength", _blurCurrent);
        _materail.SetFloat("_BlurWidth", _blurWidth);

        _materail.SetFloat("_imgHeight", ImageWidth);
        _materail.SetFloat("_imgWidth", ImageHeight);

        _materail.SetFloat("_clearSize", 10);

        Graphics.Blit(source, destination, _materail);


    }

    public void SetBlur(float amt)
    {
        _blurStrength = amt;
        _blurCurrent = _blurStrength;
        _blurRecTimer = 0;
    }
    public void HitBlur()
    {
        SetBlur(8);
    }
}
