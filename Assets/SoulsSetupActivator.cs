using UnityEngine;
using UnityEngine.SceneManagement;

public class SoulsSetupActivator : MonoBehaviour
{
    private void Awake()
    {
        if (SceneManager.GetActiveScene() == gameObject.scene)
        {
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName("Common"));
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
