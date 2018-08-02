using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This class is used to play the music during the game and the recap.
*/

public class AudioScript : MonoBehaviour {

        static AudioScript instance = null;

        void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

	// It destroy the gameObject if the Scene is the HomePage where there is a custom music which would be 
	// played at the same time. It happen if the player want to play again.

	void Update() {
		if (SceneManager.GetActiveScene ().name == "HomePage") {
			Destroy (gameObject);
		}
	}
}
