using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelExitSlow = 0.2f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(GoToScene());    
    }

    IEnumerator GoToScene()
    {
        Destroy(GameObject.Find("ScenePersist"));
        Time.timeScale = LevelExitSlow;
        yield return new WaitForSeconds(1f);
       Time.timeScale = 1f;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
