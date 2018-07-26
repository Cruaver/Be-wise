using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaking : MonoBehaviour {

	public AnsweringQuestions question;

	void OnTriggerEnter (Collider collider) {
	}

	void OnTriggerStay (Collider collider) {
		if (collider.tag == "panel") {
			if (Input.GetKey (KeyCode.Space) && question.QuestionCounter < question.maxAnswer && question.QuestionCounter < question.Questions.Length && question.QuestionCounter < question.Answers.Length) {
				collider.GetComponentInChildren<PanelText> ().PrintText ();
				question.PauseForAnswer ();
				Debug.Log ("Inside");
			}
		}
	}
}
