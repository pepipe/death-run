using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace pepipe.DeathRun
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] string m_SceneName;
        [SerializeField] GameObject m_LoadingObject;
        
        public void LoadScene()
        {
            StartCoroutine(LoadingScene());
        }
        
        IEnumerator LoadingScene()
        {
            m_LoadingObject.SetActive(true);
            var sceneAsync = SceneManager.LoadSceneAsync(m_SceneName);
            sceneAsync.allowSceneActivation = false;

            while (!sceneAsync.isDone)
            {
                if (sceneAsync.progress >= 0.9f)
                    sceneAsync.allowSceneActivation = true;

                yield return null;
            }
        }
    }
}
