using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class AnsweringQuestions : MonoBehaviour {

	private int QuestionTextIndex;

	public string[] QuestionsText = new string[]{
		"Bonjour, j'ai oublier ce petit mots pourais tu me donner ca signification ?",
		"Hello, je ne me souvien plus de ce truc aide moi, dit moi ce qu'il veux dire",
		"Bonsoir ou Bonjour petit renard, j'ai une devinette a te donner devine ce mots et je te laisserais partir.",
		"Aide moi AHHHHHH !! j'ai oublier ce mots !!!",
		"Je panique, j'ai un devoir a faire et je ne sais pas ce que veux dire ce mots !!! dit le moi je t'en suppli",
		"Bien le bonjour a toi saurais tu ce que veux dire :",
		"jouons a un jeux je demande tu devine :D"
	};

	public string json = "{\"words\":[{\"word\":\"Be\",\"correction\":\"Etre\"},{\"word\":\"Have\",\"correction\":\"Avoir\"},{\"word\":\"Other\",\"correction\":\"Autre\"},{\"word\":\"New\",\"correction\":\"Nouveau\"},{\"word\":\"Add\",\"correction\":\"Ajouter\"}],\"limit\":\"2\"}";

	//public string[] Questions = new string[]{"Be", "Have", "Other", "New", "Add"};
	//public string[] Answers = new string[]{"Etre", "Avoir", "Autre", "Nouveau", "Ajouter"};
	private QuestionObject[] words;

	public string[] Questions;
	public string[] Answers;
	static public List<string> questionList = new List<string>();

	public int AnswerOnMap = 3;
	public int actAnswerOnMap = 0;
	public int maxAnswer = 0;
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


	void Awake() {
		
		//string json = GetConfig();

		Debug.Log(json);
		words = JsonHelper.FromJson<QuestionObject>(json);
		Debug.Log(words);
		int indexer = 0;
		foreach (var word in words){
			Debug.Log(word.word);
			Debug.Log(word.correction);
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
		Debug.Log ("Question number : " + QuestionCounter);
		Debug.Log ("Question bonne : " + GoodAnswerCounter);
		Debug.Log ("Question fausse : " + falseAnswer);
		Debug.Log ("Question 1 : " + Questions[0]);
		Debug.Log ("Answer 1 : " + Answers[0]);
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
		if (actAnswerOnMap >= AnswerOnMap) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			SendScore(0);
		}
	}

	void FixedUpdate() {
	}

	public void PauseForAnswer() {
		AnswerPanel.SetActive (true);
		QuestionText.text = Questions [QuestionCounter];
		QuestionTextIndex = Random.Range (0, QuestionsText.Length);
		QuestionTextUser.text = QuestionsText [QuestionTextIndex];
		PrintErrors ();
		Time.timeScale = 0;
		NotFind = true;
	}

	public void UnPause() {
		questionList.Add(Questions[QuestionCounter]);
		falseAnswer = 0;
		QuestionCounter++;
		actAnswerOnMap++;
		AnswerPanel.SetActive (false);
		Time.timeScale = 1;
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