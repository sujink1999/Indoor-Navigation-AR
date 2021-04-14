using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateChange : MonoBehaviour {

	public UnityEngine.Events.UnityEvent isLight;
	public UnityEngine.Events.UnityEvent isDark;
	public float value = 0.1f;
	private bool isDarkPrevious;
	// Use this for initialization
	void Start () {
        isLight.Invoke();
    }


	// Update is called once per frame
	void Update () {


		 if (value < 0.4) {
			 isLight.Invoke();
		 }
		 else
		 {
			 isDark.Invoke();
		 }


	}

}