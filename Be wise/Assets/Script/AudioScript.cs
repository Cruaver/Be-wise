using UnityEngine;

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
}
