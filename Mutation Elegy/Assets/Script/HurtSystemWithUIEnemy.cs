using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HurtSystemWithUIEnemy : HurtSystem
{
    [Header("要更新的血條")]
    public Image imgHp;
    private float hpEffectOriginal;
    private void OnEnable()
    {
        imgHp = GetComponent<HealthBarUI>().healthSlider;
    }
    public override bool Hurt(float damage)
    {
        hpEffectOriginal = hp;

        base.Hurt(damage);
        StartCoroutine(HpBarEffect());

        return hp <= 0;
    }
    private IEnumerator HpBarEffect()
    {
        while(hpEffectOriginal != hp)
        {
            hpEffectOriginal--;
            imgHp.fillAmount = hpEffectOriginal / hpMax;
            yield return new WaitForSeconds(0.01f);
        }
    }

}