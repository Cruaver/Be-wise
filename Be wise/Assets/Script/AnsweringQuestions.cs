using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AnsweringQuestions : MonoBehaviour {

	public string[] Questions = new string[]{"Be", "Have", "Other", "New", "Add"};
	public string[] Answers = new string[]{"Etre", "Avoir", "Autre", "Nouveau", "Ajouter"};
	static public List<string> questionList = new List<string>();


	public int maxAnswer = 0;
	public int maxFalse = 3;
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
			if (maxAnswer > 0 && falseAnswer < maxFalse && NotFind && QuestionCounter < Questions.Length && QuestionCounter < Answers.Length) {
				if (inputed.text != "" && Input.GetKey (KeyCode.Return)) {
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
		QuestionText.text = Questions [QuestionCounter];
		PrintErrors ();
		Time.timeScale = 0;
		NotFind = true;
	}

	public void UnPause() {
		questionList.Add(Questions[QuestionCounter]);
		falseAnswer = 0;
		QuestionCounter++;
		AnswerPanel.SetActive (false);
		Time.timeScale = 1;
		//SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	private void PrintErrors() {
		string error = "";
		if (maxAnswer > 0 && falseAnswer < maxFalse) {
			int i = 0;
			while (i < maxFalse) {
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
			NotFind = false;
		} else {
			falseAnswer++;
		}
		PrintErrors ();
		inputed.text = "";
	}
}