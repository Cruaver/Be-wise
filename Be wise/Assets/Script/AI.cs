using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This class is used for the movement of NPCs and verify if the player have already interacted with them
*/

public class AI : MonoBehaviour {

	public bool speaked = false;


    private RaycastHit Hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * 3 * Time.deltaTime);

        if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out Hit,2))
        {
            transform.Rotate(Vector3.up * Random.Range(90, 180));
        }

		// this part disappear the Speech Bubble from the NPCs.

		if (speaked && transform.Find ("Bubble").gameObject.activeSelf) {
			transform.Find ("Bubble").gameObject.SetActive (false);
		}

	}
}
