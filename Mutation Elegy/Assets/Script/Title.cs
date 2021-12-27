using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{
    [Header("設定選單")]
    public GameObject Option;

    [Header("素材選單")]
    public GameObject resources;

    // Start is called before the first frame update
    void Start()
    {
        int isPass = PlayerPrefs.GetInt(StaticVal.isPass);
        //int isPass;
        //isPass = 1;
        if (isPass == 1)
        {
            resources.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LV_01_Forest_v2");
    }

    public void GoToOption()
    {
        Option.SetActive(true);
    }

    public void GoToResources()
    {
        SceneManager.LoadScene("Resources");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
