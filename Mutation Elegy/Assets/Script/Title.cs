using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [Header("�]�w���")]
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
