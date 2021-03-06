﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This class is suposed to be added to the gameObjects when we want to
 * use random gameObjects that are added to the GameObject array "Models"
 * It is actually not used
*/

public class RandomSpawn : MonoBehaviour {

	public string[] Questions = new string[5];
	public GameObject[] Models = new GameObject[5];

	// Use this for initialization
	void Start () {
		int random = Random.Range (0, Questions.Length);
		string randomString = Questions[random];
		Debug.Log (randomString);
		GameObject theObject = Models [random];
		GetComponent<MeshFilter> ().sharedMesh = theObject.GetComponent<MeshFilter> ().sharedMesh;
		GetComponent<MeshCollider> ().sharedMesh = theObject.GetComponent<MeshCollider> ().sharedMesh;
		GetComponent<MeshRenderer> ().sharedMaterials = theObject.GetComponent<MeshRenderer> ().sharedMaterials;
		print (theObject.name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
