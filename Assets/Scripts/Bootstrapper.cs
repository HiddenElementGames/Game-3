using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper
{
    private const int MANAGER_SCENE_INDEX = 1; // index found in build settings

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        SceneManager.LoadSceneAsync(MANAGER_SCENE_INDEX, LoadSceneMode.Additive);
    }
}
