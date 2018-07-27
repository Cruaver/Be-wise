using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AnsweringQuestions : MonoBehaviour {

	public string[] Questions = new string[]{"Be", "Have", "Other", "New", "Add"};
	public string[] Answers = new string[]{"Etre", "Avoir", "Autre", "Nouveau", "Ajouter"};


	public int maxAnswer = 0;
	public bool NotFind = true;

	public GameObject AnswerPanel;
	public Text QuestionText, ErrorsText;

	static public int QuestionCounter = 0;
	static public int GoodAnswerCounter = 0;
	static public int falseAnswer = 0;

	public InputField inputed;

	// Use this for initialization
	void Start () {
		AnswerPanel.SetActive (false);
		Debug.Log ("question number : " + QuestionCounter);
		Debug.Log ("question bonne : " + GoodAnswerCounter);
		Debug.Log ("question fausse : " + falseAnswer);
	}

	// Update is called once per frame
	void Update () {
		if (AnswerPanel.activeSelf == true) {
			if (maxAnswer > 0 && falseAnswer < maxAnswer && NotFind && QuestionCounter < Questions.Length && QuestionCounter < Answers.Length) {
				if (inputed.isFocused && inputed.text != "" && Input.GetKey (KeyCode.Return)) {
					IsGoodAnswer();
				}
			} else {
				UnPause ();
			}
		}
	}

	void FixedUpdate() {
	}

	public void PauseForAnswer() {
		AnswerPanel.SetActive (true);
		PrintErrors ();
		Time.timeScale = 0;
		NotFind = true;
		QuestionText.text = Questions [QuestionCounter];
	}

	public void UnPause() {
		falseAnswer = 0;
		AnswerPanel.SetActive (false);
		Time.timeScale = 1;
	}

	private void PrintErrors() {
		string error = "";
		if (maxAnswer > 0 && falseAnswer < maxAnswer) {
			int i = 0;
			while (i < maxAnswer) {
				while (i < falseAnswer) {
					error += "X ";
					i++;
				}
				error += (i + 1 < maxAnswer) ? "_ " : "_";
				i++;
			}
		}
		ErrorsText.text = error;
	}

	private void IsGoodAnswer() {
		if (inputed.text == Answers[QuestionCounter]) {
			GoodAnswerCounter++;
			QuestionCounter++;
			NotFind = false;
			Time.timeScale = 1 ;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		} else {
			falseAnswer++;
			if (maxAnswer > 0 && falseAnswer >= maxAnswer) {
				QuestionCounter++;
			}
		}
		PrintErrors ();
		inputed.text = "";
	}
}