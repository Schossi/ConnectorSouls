using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// when its gameobject is in the active scene it moves itself to the Common scene that never gets unloaded<br/>
/// when in an inactive scene it just destroys its gameobject<br/>
/// this enables having a Setup object in every scene that can be used to start from while debugging
/// </summary>
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
