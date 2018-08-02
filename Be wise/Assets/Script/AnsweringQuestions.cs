using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

/*
 * This class is used on the game for the request of the NPCs, the verification of the user's inputs and all the proceedings of the game.
*/

public class AnsweringQuestions : MonoBehaviour {

	// It is the values for a random request of the NPCs
	private int QuestionTextIndex;
	public string[] QuestionsText = new string[]{
		"Bonjour, j'ai oublié ce petit mot pourrais-tu me donner sa signification ?",
		"Hello, je ne me souviens plus de ce truc. Aide-moi, dis moi ce qu'il veut dire",
		"Bonsoir ou bonjour petit renard, j'ai une devinette à te donner. Devine ce mot et je te laisserai partir.",
		"Aide moi ! AHHHHHH !! J'ai oublié ce mot !!!",
		"Je panique, j'ai un devoir à faire et je ne sais pas ce que veut dire ce mot !!! Dis-le moi je t'en supplie",
		"Bien le bonjour à toi saurais-tu ce que veut dire :",
		"Jouons à un jeu ! Je te demande quelque chose, tu devines :D"
	};


	// Test unity uncoment this line
	public string json = "{\"words\":[{\"word\":\"Be\",\"correction\":\"Etre\"},{\"word\":\"Have\",\"correction\":\"Avoir\"},{\"word\":\"Other\",\"correction\":\"Autre\"},{\"word\":\"New\",\"correction\":\"Nouveau\"},{\"word\":\"Add\",\"correction\":\"Ajouter\"}],\"limit\":\"5\"}";
	private QuestionObject[] words;

	public string[] Questions;
	public string[] Answers;

	public int AnswerOnMap = 3;
	public int actAnswerOnMap = 0;
	public int maxAnswer;
	public int maxFalse = 3;
	public bool NotFind = true;
	public GameObject AnswerPanel;
	public Text QuestionText, ErrorsText, QuestionTextUser;
	public GameObject Music;

	static public int QuestionCounter = 0;
	static public int GoodAnswerCounter = 0;
	static public int falseAnswer = 0;

	public InputField inputed;

	[DllImport("__Internal")]
	private static extern void SendScore(int score);
	private static extern string GetConfig();


	// on awake, it will parse the values from a JSON that should be received from the teacher.
	void Awake() {

		//Test unity comment this line
		//string json = GetConfig();

		words = JsonHelper.FromJson<QuestionObject>(json);
		int indexer = 0;
		foreach (var word in words){
			Questions[indexer] = word.word;
			Answers[indexer] = word.correction;
			indexer = indexer + 1;
		}
		// Get the limit send by the json and save it in GameManager
		maxAnswer = int.Parse(JsonUtility.FromJson<Limit>(json).limit);
	}

	// Use this for initialization
	void Start () {
		AnswerPanel.SetActive (false);
		Music.SetActive (true);
		if (StaticClass.Correct == null) {
			StaticClass.Correct = new bool[maxAnswer];
		}
		StaticClass.Questions = Questions;
		StaticClass.Answers = Answers;
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

		// this part reload the scene if there is not NPCs anymore with which the player could interact.

		if (actAnswerOnMap >= AnswerOnMap) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}

		// This part check if the player has answer to all the questions and send the score to the teacher.

		if (QuestionCounter >= maxAnswer) {
			StaticClass.Score = (int)(((float)GoodAnswerCounter / (float)maxAnswer) * 20);
            #if UNITY_WEBGL
				SendScore (StaticClass.Score);
            #endif
            QuestionCounter = 0;
			SceneManager.LoadScene ("Recap");
		}
	}

	// this part allow the player to answer to the question

	public void PauseForAnswer() {
		AnswerPanel.SetActive (true);
		QuestionText.text = Questions [QuestionCounter];
		QuestionTextIndex = Random.Range (0, QuestionsText.Length);
		QuestionTextUser.text = QuestionsText [QuestionTextIndex];
		PrintErrors ();
		Time.timeScale = 0;
		NotFind = true;
	}

	// this function enable the player and the NPCs to move if the answer has well answered to the question or if he don't have enough try. 

	public void UnPause() {
		StaticClass.Correct [QuestionCounter] = (falseAnswer >= maxFalse) ? false : true;
		falseAnswer = 0;
		QuestionCounter++;
		actAnswerOnMap++;
		AnswerPanel.SetActive (false);
		Time.timeScale = 1;
	}

	// this function print the errors that was made by the player.

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

	// this function is use to check if the player has well answered the question.

	private void IsGoodAnswer() {
		if (inputed.text.ToLower() == Answers[QuestionCounter].ToLower()) {
			GoodAnswerCounter++;
			NotFind = false;
		} else {
			falseAnswer++;
		}
		PrintErrors ();
		inputed.text = "";
	}
}

// This class is used to serialize a json array, which is not possible with the native JsonUtility class.
public static class JsonHelper
{
	public static T[] FromJson<T>(string json)
	{
		Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
		return wrapper.words;
	}

	public static string ToJson<T>(T[] array)
	{
		Wrapper<T> wrapper = new Wrapper<T>();
		wrapper.words = array;
		return JsonUtility.ToJson(wrapper);
	}

	public static string ToJson<T>(T[] array, bool prettyPrint)
	{
		Wrapper<T> wrapper = new Wrapper<T>();
		wrapper.words = array;
		return JsonUtility.ToJson(wrapper, prettyPrint);
	}

	[System.Serializable]
	private class Wrapper<T>
	{
		public T[] words;
	}
}