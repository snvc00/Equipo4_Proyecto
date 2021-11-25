using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoTransition : MonoBehaviour
{
    public int VideoDuration;
    public int NextSceneIndex;

    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(WaitAndLoadScene());
    }

    private IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(VideoDuration);

        SceneManager.LoadScene(NextSceneIndex);
    }
}
