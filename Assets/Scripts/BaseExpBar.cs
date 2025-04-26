using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseExpBar : MonoBehaviour
{
    public Image expBar;
    public TextMeshProUGUI expText;
    BillionBase bb;
    private void Awake()
    {
        bb = GetComponent<BillionBase>();
    }

    public void setExp(int xp)
    {
        Debug.Log("setExp is triggered");
        expText.SetText(xp.ToString());
        expBar.fillAmount = (float)bb.exp / (float)bb.maxExp;
    }
}
