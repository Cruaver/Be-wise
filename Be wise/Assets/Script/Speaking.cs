using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is used to Spek with the NPCs
*/

public class Speaking : MonoBehaviour {

	public AnsweringQuestions question;

	void OnTriggerEnter (Collider collider) {
	}

	void OnTriggerStay (Collider collider) {
		if (collider.tag == "panel" && !collider.GetComponent<AI>().speaked) {
			if (Input.GetKey (KeyCode.Space) && AnsweringQuestions.QuestionCounter < question.maxAnswer && AnsweringQuestions.QuestionCounter < question.Questions.Length && AnsweringQuestions.QuestionCounter < question.Answers.Length) {
					question.PauseForAnswer ();
					collider.GetComponent<AI> ().speaked = true;
				}
		}
	}
}
