
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryCanvas : MonoBehaviour
{
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("LV_01_Forest_v1");
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("LV_01_Forest_v1");
    }
}