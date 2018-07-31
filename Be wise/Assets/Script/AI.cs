using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {


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
	}
}
