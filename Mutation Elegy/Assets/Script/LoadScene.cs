using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTitle : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("Title");
    }
}
