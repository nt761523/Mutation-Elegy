using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject menuui;
    public GameObject menuicon;
    public ThirdCamera cam;
    bool cammove;
    // Start is called before the first frame update
    void Start()
    {
        MouseState(false);
        cammove = true;
    }

    // Update is called once per frame
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
    }
    private void LateUpdate()
    {
        cam.CamControl(cammove);
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
