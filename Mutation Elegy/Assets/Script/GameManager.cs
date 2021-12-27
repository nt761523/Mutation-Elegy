using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject menuui;
    public GameObject deadui;
    public GameObject menuicon;
    public ThirdCamera cam;
    //public GameObject test;
    public PlayableDirector bosscut;
    //public Controller controller;
    public GameObject player;
    public AudioSource BGM;
    public AudioClip BGMclip;

    bool cammove;
    void Start()
    {
        MouseState(false);
        cammove = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))

        {
            Time.timeScale = 0;
            menuui.SetActive(true);
            MouseState(true);
            cammove = false;
            menuicon.SetActive(false);
        }
        if(StaticVal.Enemykilled == 6)
        {
            //test.SetActive(true);
            StaticVal.Enemykilled = 0;

            SceneManager.LoadScene("LV_01_Forest_Boss");
            //print("Boss戰");
            //bosscut.Play();
            //player.transform.position = new Vector3(38, 0, 21);
            //BGM.clip = BGMclip;
            //BGM.Play();
            //BGM.name = "魔王魂 BGM ネオロック83";

        }
    }
    private void LateUpdate()
    {
        cam.CamControl(cammove);
    }
    public void EndScene()
    {
        bosscut.Play();
    }
    public void BackToGame()
    {
        Time.timeScale = 1;
        menuui.SetActive(false);
        MouseState(false);
        cammove = true;
        menuicon.SetActive(true);
    }
    public void BackToTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
        MouseState(true);
    }
    public void EndGame()
    {
        Application.Quit();
        MouseState(true);
    }
    public void calldeadui()
    {
        //Time.timeScale = 0;
        deadui.SetActive(true);
        MouseState(true);
        cammove = false;
        menuicon.SetActive(false);
    }
    public void Regame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LV_01_Forest_v2");
        MouseState(true);
    }
    public void RegameBoss()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LV_01_Forest_Boss");
        MouseState(true);
    }

    private void MouseState(bool onoff)
    {
        if (onoff)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
}
