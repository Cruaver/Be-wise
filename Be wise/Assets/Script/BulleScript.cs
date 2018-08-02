using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulleScript : MonoBehaviour {

	public Transform LookTarget;
	public bool check = true;

	void Start () {

	}


	void Update () {
		//Rotation de la bulle vers la target
		Vector3 targetPosition = new Vector3(LookTarget.position.x, transform.position.y, LookTarget.position.z);
		transform.LookAt(targetPosition);
	}
}
