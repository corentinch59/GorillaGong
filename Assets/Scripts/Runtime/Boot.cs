using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GorillaGong.Runtime
{
    // TODO: ASYNC stuff not pretty here
    public class Boot : MonoBehaviour
    {
        [SerializeField] private string[] scenesToLoad;

        private void Start()
        {
            LoadScenes();
        }

        private void LoadScenes()
        {
            foreach (string sceneName in scenesToLoad)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }

        public void UnloadAll()
        {
            UnloadAllAsync();
        }
        private async Task UnloadAllAsync()
        {
            await UnloadScenesAsync();
            DestroyAllGameObjects();
        }

        private void DestroyAllGameObjects()
        {
            GameObject[] sceneGameObjects = FindObjectsOfType<GameObject>(true);
            foreach (GameObject sceneGameObject in sceneGameObjects)
            {
                if (sceneGameObject == gameObject)
                {
                    continue;
                }
                
                Destroy(sceneGameObject);
            }
        }

        private async Task UnloadScenesAsync()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene == gameObject.scene)
                {
                    continue;
                }

                await WaitSceneUnload(scene);
                i--;
            }
        }

        private async Task WaitSceneUnload(Scene scene)
        {
            bool wait = true;
            SceneManager.UnloadSceneAsync(scene).completed += _ => wait = false;

            while (true)
            {
                await Task.Delay(100);
                if (!wait)
                {
                    return;
                }
            }
        }

        public void ReloadScenes()
        {
            ReloadScenesAsync();
        }
        private async Task ReloadScenesAsync()
        {
            await UnloadAllAsync();
            LoadScenes();
        }

    }
}