using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void NextLevel()
    {
        StartCoroutine(LevelLoad(SceneManager.GetActiveScene().buildIndex + 1));
    }


    IEnumerator LevelLoad(int levelIndex)
    {
        transition.SetTrigger("FadeIn");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
        FindObjectOfType<AudioManager>().Play("PlayerEntrance");
    }

}
