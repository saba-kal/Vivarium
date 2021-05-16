using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages activation and disabling of various cut-scenes at the start of a level.
/// </summary>
public class CutSceneManager : MonoBehaviour
{
    private const string BLACK_BACKGROUND_NAME = "BlackBackgroundCanvas";

    public static CutSceneManager Instance { get; private set; }

    public CinemachineVirtualCamera MainVirtualCamera;

    private List<GameObject> _cutSceneObjects = new List<GameObject>();
    private GameObject _canvasGameObject;
    private bool _uiVisible = true;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        GetCutScenes();
    }

    private void Update()
    {
        SetUIVisible(_uiVisible);
    }

    public void ActivateCutScene(string name)
    {
        GetCutScenes();

        foreach (var cutScene in _cutSceneObjects)
        {
            if (cutScene.name == name)
            {
                StartCoroutine(DisableCutScene(cutScene));
                MainVirtualCamera.gameObject.SetActive(false);
                _uiVisible = false;
                cutScene.SetActive(true);
                return;
            }
        }
    }

    private void GetCutScenes()
    {
        if (_cutSceneObjects != null && _cutSceneObjects.Count > 0)
        {
            return;
        }

        _cutSceneObjects = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == BLACK_BACKGROUND_NAME)
            {
                continue;
            }

            child.gameObject.SetActive(false);
            _cutSceneObjects.Add(child.gameObject);
        }
    }

    private IEnumerator DisableCutScene(GameObject cutScene)
    {
        var cutSceneDuration = cutScene.GetComponent<CutScene>()?.Duration ?? 0f;

        yield return new WaitForSeconds(cutSceneDuration - 1);

        _uiVisible = true;

        yield return new WaitForSeconds(1);

        cutScene.SetActive(false);
        MainVirtualCamera.gameObject.SetActive(true);
    }

    private void SetUIVisible(bool isVisible)
    {
        if (_canvasGameObject == null)
        {
            _canvasGameObject = GameObject.FindWithTag(Constants.CANVAS_TAG);
        }

        _canvasGameObject?.SetActive(isVisible);
    }
}
