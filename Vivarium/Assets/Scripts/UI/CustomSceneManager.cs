using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{

    public List<SceneCollection> AvailableScenes;

    private bool _created = false;

    void Awake()
    {
        Debug.Log("Awake:" + SceneManager.GetActiveScene().name);

        // Ensure the script is not deleted while loading.
        if (!_created)
        {
            DontDestroyOnLoad(this.gameObject);
            _created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void LoadScene(int sceneCollectionIndex)
    {
        if (sceneCollectionIndex < 0 || sceneCollectionIndex >= AvailableScenes.Count)
        {
            Debug.LogError("Scene index is out of range.");
            return;
        }

        StartCoroutine(LoadLevel(AvailableScenes[sceneCollectionIndex]));
    }

    IEnumerator LoadLevel(SceneCollection sceneCollection)
    {
        yield return SceneManager.LoadSceneAsync(sceneCollection.MainSceneName);

        foreach (var additiveScene in sceneCollection.AdditiveScenes)
        {
            var scene = SceneManager.GetSceneByName(additiveScene);
            if (scene == null)
            {
                print("Scene is null");
                continue;
            }

            if (scene.isLoaded)
            {
                print("Scene is loaded");
                yield return SceneManager.UnloadSceneAsync(scene);
                continue;
            }

            print("Loading scene");
            yield return SceneManager.LoadSceneAsync(additiveScene, LoadSceneMode.Additive);
        }

        Destroy(this.gameObject);
    }
}

[Serializable]
public class SceneCollection
{
    public string MainSceneName;
    public List<string> AdditiveScenes;
}
