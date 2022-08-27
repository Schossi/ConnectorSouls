using UnityEngine;
using UnityEngine.SceneManagement;

public class SetupActivator : MonoBehaviour
{
    private void Awake()
    {
        if (gameObject.scene.name == "ConnectorSouls")
            return;//startup in build has no Common

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
