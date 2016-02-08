using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderTemp : MonoBehaviour {

    public float temp;
    [SerializeField]
    private Slider tempSlider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        TempMeter();
	}
    void TempMeter()
    {
        float handleValue = (temp + 100.0f) / 200.0f;
        tempSlider.value = handleValue;
    }
}
