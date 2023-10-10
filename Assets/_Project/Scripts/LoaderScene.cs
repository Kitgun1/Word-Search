using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class LoaderScene : MonoBehaviour
    {
        public void LoadScene()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            YandexGamesSdk.GameReady();
#endif
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }
    }
}