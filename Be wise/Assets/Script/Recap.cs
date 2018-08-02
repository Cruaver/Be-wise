using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Recap : MonoBehaviour {
	public Text theText;
	public GameObject previous, next;

	private string[] text;
	private string theContent = "";
	private int theLine;

	// Use this for initialization
	void Start () {
		theLine = 0;
		int max = (StaticClass.Correct.Length / 10) + 1;
		text = new string[max];
		int line = 0;
		int i = 0;
		while (StaticClass.Questions.Length > i && StaticClass.Answers.Length > i && StaticClass.Correct.Length > i) {
			theContent += StaticClass.Questions [i] + " : ";
			if (StaticClass.Correct [i])
				theContent += "<color=#668FFF>" + StaticClass.Answers [i] + "</color>\n\n";
			else
				theContent += "<color=#FF0000>" + StaticClass.Answers [i] + "</color>\n\n";
			i++;
			if (i % 10 == 0) {
				Debug.Log (line);
				text [line] = theContent;
				line++;
				theContent = "";
			}
		}
		text [line] = theContent;
		theText.text = text[0];
		buttonAvailable ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BackHome() {
		SceneManager.LoadScene ("HomePage");
	}

	public void changeText(int direction) {
		if (direction >= 0)
			theLine++;
		else
			theLine -= 1;
		theText.text = text [theLine];
		buttonAvailable ();
	}

	public void buttonAvailable() {
		if (theLine > 0)
			previous.SetActive (true);
		else
			previous.SetActive(false);
		if (theLine < (text.Length - 1))
			next.SetActive(true);
		else
			next.SetActive(false);
	}
}
