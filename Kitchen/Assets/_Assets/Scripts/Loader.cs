using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    public enum Scene
    {
        MainMenuScene = 0,
        GameScene = 1,
        LoadingScene = 2
    }

    private static Scene targetScene;


    public static void Load(Scene scene)
    {
        Loader.targetScene = scene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
