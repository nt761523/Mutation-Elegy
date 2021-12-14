using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public GameObject healthUIPrefab;
    public Transform barPoint;
    public Canvas enemyHPCanvas;

    public bool alwaysVisible;

    public float visibleTime;
    public float timeLeft;

    public Image healthSlider;
    Transform UIbar;
    Transform cam;

    private void Awake()
    {
        //GameObject temp = GameObject.Find("EnemyHPCanvas").GetComponent<Canvas>();
        enemyHPCanvas = GameObject.Find("EnemyHPCanvas").GetComponent<Canvas>();
    }
    private void OnEnable()
    {
        cam = Camera.main.transform;
        UIbar = Instantiate(healthUIPrefab, enemyHPCanvas.transform).transform;
        healthSlider = UIbar.GetChild(0).GetComponent<Image>();
        UIbar.gameObject.SetActive(alwaysVisible);
    }
    public void UpdateHealthBar()
    {
        //Destroy(UIbar.gameObject);
        UIbar.gameObject.SetActive(true);
        timeLeft = visibleTime;

        //float sliderPercent = (float)currentHealth / maxHealth;
        //healthSlider.fillAmount = -sliderPercent;
    }
    private void LateUpdate()
    {
        if(UIbar != null)
        {
            UIbar.position = barPoint.position;
            UIbar.forward = -cam.forward;
            if(timeLeft <= 0 && !alwaysVisible)
                UIbar.gameObject.SetActive(false);
            else
                timeLeft -= Time.deltaTime;
        }
    }
}
