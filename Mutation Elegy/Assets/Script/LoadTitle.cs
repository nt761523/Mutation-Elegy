using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerPrefs.SetInt(StaticVal.isPass, 1);
        SceneManager.LoadScene("ProductionList");
    }
}
