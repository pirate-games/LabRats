using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public static class Loader
{
    public enum Scene
    {
        CreateGame,
        Lab,
        Loading,
    }
    public static void LoadNetwork(Scene scene)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
    }
    public static void LoadAsync(Scene scene)
    {
        SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Single);
    }
}
