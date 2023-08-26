using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SceneLoader : MonoBehaviour
{
    /* public static SceneLoader Instance;

     [SerializeField] private GameObject _loaderCanvas;
     [SerializeField] private Image _progressbar;
     */
    /*void Awake()
    {*/
        /*if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }*/
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /*public async void LoadScene(string SceneName)
        {
             var scene = SceneManager.LoadSceneAsync(SceneName);
            scene.allowSceneActivation = false;

            _loaderCanvas.SetActive(true);

            // check how much scene loaded
            do
            {
                // If scene load immediately, can add the code below
                await Task.Delay(100);
                _progressbar.fillAmount = scene.progress;
            } while (scene.progress < 0.9f);

            scene.allowSceneActivation(true);
            loaderCanvas.SetActive(false);

        }*/

        public void changeScene(string scene_name)
        {
            SceneManager.LoadScene(scene_name);
        }

        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("Quit!");
        }
}
