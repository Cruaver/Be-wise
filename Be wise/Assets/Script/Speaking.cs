using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaking : MonoBehaviour {

	public AnsweringQuestions question;

	void OnTriggerEnter (Collider collider) {
	}

	void OnTriggerStay (Collider collider) {
		if (collider.tag == "panel") {
			if (Input.GetKey (KeyCode.Space) && AnsweringQuestions.QuestionCounter < question.maxAnswer && AnsweringQuestions.QuestionCounter < question.Questions.Length && AnsweringQuestions.QuestionCounter < question.Answers.Length) {
				//collider.GetComponentInChildren<PanelText> ().PrintText ();
				question.PauseForAnswer ();
				Debug.Log ("Inside");
			}
		}
	}
}
