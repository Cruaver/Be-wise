using UnityEngine;
using UnityEngine.SceneManagement;

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

	void Update() {
		if (SceneManager.GetActiveScene ().name == "HomePage") {
			Destroy (gameObject);
		}
	}
}
