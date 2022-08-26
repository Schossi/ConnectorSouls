using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Placeholder : MonoBehaviour
{
    [Tooltip("when this scene is loaded the placeholder gets deactivated, when it is unloaded it gets activated")]
    public string SceneName;
    [Tooltip("whether the deactivation should be delayed by a couple frames because the counterpart is not in place yet")]
    public bool Delay;

    private void Awake()
    {
        SceneManager.sceneLoaded += sceneLoaded;
        SceneManager.sceneUnloaded += sceneUnloaded;

        gameObject.SetActive(!SceneManager.GetSceneByName(SceneName).isLoaded);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= sceneLoaded;
        SceneManager.sceneUnloaded -= sceneUnloaded;
    }

    private void sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == SceneName)
        {
            if (Delay)
                StartCoroutine(deactivateDelayed());
            else
                gameObject.SetActive(false);
        }
    }

    private void sceneUnloaded(Scene scene)
    {
        if (scene.name == SceneName)
            gameObject.SetActive(true);
    }

    private IEnumerator deactivateDelayed()
    {
        yield return null;
        yield return null;
        yield return null;
        gameObject.SetActive(false);
    }
}
