using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaking : MonoBehaviour
{

    public AnsweringQuestions question;

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(AnsweringQuestions.questionList.Contains(question.QuestionText.text));
        print("--------------------");
        foreach (string test in AnsweringQuestions.questionList)
        {
            Debug.Log(test);
        }
        print("--------------------");
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "panel")
        {
            if (AnsweringQuestions.questionList.Contains(question.QuestionText.text) == false)
            {
                if (Input.GetKey(KeyCode.Space) && AnsweringQuestions.QuestionCounter < question.maxAnswer && AnsweringQuestions.QuestionCounter < question.Questions.Length && AnsweringQuestions.QuestionCounter < question.Answers.Length)
                {
                    //collider.GetComponentInChildren<PanelText>().PrintText();
                    question.PauseForAnswer();
                }
            }
            else if (AnsweringQuestions.questionList.Contains(question.QuestionText.text) == true)
            {
                Debug.Log("pas possible");
            }
        }
    }
}
