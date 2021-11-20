using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{
    [Header("設定選單")]
    public GameObject Option;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LV_01_Forest");
    }

    public void GoToOption()
    {
        Option.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
