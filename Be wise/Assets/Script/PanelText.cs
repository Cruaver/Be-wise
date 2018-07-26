using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelText : MonoBehaviour {

	public string word;

	private TextMesh text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PrintText() {
		text = GetComponentInChildren<TextMesh> ();

		text.text = word;
	}
}
