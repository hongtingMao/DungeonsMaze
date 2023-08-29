using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string LevelName;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (SceneLoader.Instance != null)
            {
                SceneLoader.Instance.LoadScene(LevelName);
            }
            else
            {
                // Fallback in case SceneLoader instance is not available
                SceneManager.LoadScene(LevelName);
            }
        }
    }
}
