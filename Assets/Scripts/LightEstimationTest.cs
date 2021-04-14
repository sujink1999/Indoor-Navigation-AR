using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class LightEstimationTest : MonoBehaviour {

    [Range(0f, 1f)]
    public float testValue = 0.5f;

    private void OnValidate()
    {
        setGlobalLightEstimation(testValue);
    }

    void setGlobalLightEstimation(float lightValue)
    {
        Shader.SetGlobalFloat("_GlobalLightEstimation", lightValue);

    }

    void Update () {
        setGlobalLightEstimation(Frame.LightEstimate.PixelIntensity);

	}
}
