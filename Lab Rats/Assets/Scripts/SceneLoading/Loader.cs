using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using System;
using Global.Tools;

public class Loader : Singleton<Loader>
{
    public enum Scene
    {
        CreateGame,
        Lab,
        Loading,
    }
    public async void LoadNetwork(Scene scene)
    {
        var m_SceneName = scene.ToString();

        var succeeded = NetworkManager.Singleton.SceneManager != null;
        Debug.Log(succeeded);
        if (!string.IsNullOrEmpty(m_SceneName) && succeeded)
        {
            var status = NetworkManager.Singleton.SceneManager.LoadScene(m_SceneName, LoadSceneMode.Single);
            if (status != SceneEventProgressStatus.Started)
            {
                Debug.LogWarning($"Failed to load {m_SceneName} " +
                      $"with a {nameof(SceneEventProgressStatus)}: {status}");
            }
        }
    }
    //public static void LoadAsync(Scene scene)
    //{
    //    SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Single);
    //}
}
