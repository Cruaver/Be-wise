using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This class is used on the Scene "HomePage". It is used to change the Scene.
*/

public class HomePage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			SceneManager.LoadScene ("V1");
		}
	}
}
