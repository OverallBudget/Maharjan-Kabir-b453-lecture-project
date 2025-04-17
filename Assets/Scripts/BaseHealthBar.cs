using UnityEngine;
using UnityEngine.UI;

public class BaseHealthBar : MonoBehaviour
{
    public Image hpBar;
    BillionBase bb;
    private void Awake()
    {
        bb = GetComponent<BillionBase>();
    }

    public void setHealth()
    {
        Debug.Log("setHealth is triggered");
        hpBar.fillAmount = (float)bb.Hp / (float)bb.maxHp;
    }
}
