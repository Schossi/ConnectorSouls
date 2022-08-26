using UnityEngine.SceneManagement;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

/// <summary>
/// special version of SceneConnector that adds handling for the 'Temp' scenes AdventureSouls uses for objects that are reset when resting<br/>
/// </summary>
public class SoulsConnector : SceneConnector
{
#if UNITY_EDITOR
    static SoulsConnector()
    {
        SceneConnectorCore.ConnectorGraphModel.SceneLoaded += (sceneModel, connectorModel) =>
        {
            SceneAsset scene;
            if (connectorModel == null)
                scene = sceneModel.Scene;
            else
                scene = connectorModel.Scene;

            var path = AssetDatabase.GetAssetPath(scene);
            if (path.Contains("Loading"))
                return;

            EditorSceneManager.OpenScene(path.Replace(".unity", "Temp.unity"), OpenSceneMode.Additive);
        };
        SceneConnectorCore.ConnectorGraphModel.CollectingBuildScenes += (scenes) =>
        {
            foreach (var scene in scenes.ToArray())
            {
                if (SceneConnectorCore.ConnectorGraphModel.Instance.AdditionalScenes.Any(s => AssetDatabase.GetAssetPath(s) == scene.path))
                    continue;
                if (scene.path.Contains("Loading"))
                    continue;

                var temp = scene.path.Replace(".unity", "Temp.unity");
                if (scenes.Any(s => s.path == temp))
                    continue;
                scenes.Add(new EditorBuildSettingsScene(temp, true));
            }
        };
    }
#endif

    public override void Load()
    {
        base.Load();

        if (!SceneManager.GetSceneByName(SceneName + "Temp").isLoaded)
            SceneManager.LoadSceneAsync(SceneName + "Temp", LoadSceneMode.Additive);
    }

    public override void Unload()
    {
        base.Unload();

        if (SceneManager.GetSceneByName(SceneName + "Temp").isLoaded)
            SceneManager.UnloadSceneAsync(SceneName + "Temp");
    }

    public override void Traverse()
    {
        base.Traverse();

        if (SceneManager.GetSceneByName(SceneName + "Temp").isLoaded)
            SceneManager.UnloadSceneAsync(SceneName + "Temp");
    }

#if UNITY_EDITOR
    public override void EditorOpen()
    {
        base.EditorOpen();

        EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(Scene).Replace(".unity", "Temp.unity"), OpenSceneMode.Additive);
    }

    public override void EditorClose()
    {
        base.EditorClose();

        EditorSceneManager.CloseScene(SceneManager.GetSceneByName(SceneName + "Temp"), true);
    }

    public override void EditorCloseSelf()
    {
        EditorSceneManager.CloseScene(SceneManager.GetSceneByName(gameObject.scene.name + "Temp"), true);

        base.EditorCloseSelf();
    }
#endif
}
