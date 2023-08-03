using UnityEngine;
using UnityEngine.UI;
public class EnemyVisual : CardVisual, ISeletableTarget
{
    [SerializeField]
    Image selectableEdge;
    [SerializeField] Text healthText;
    [SerializeField] Text nameText;
    [SerializeField] Text descText;
    [SerializeField] Text countDownText;
    [SerializeField] Slider runeProgressBar;

    #region ISeletableTarget实现区域
    public void UpdateSelectableVisual(bool canSelect)
    {
        if (canSelect)
            selectableEdge.color = Color.green;
        else selectableEdge.color = Color.white;
    }
    #endregion

    public override void UpdateVisual()
    {
        if (nameText) nameText.text = card.name;
        if (healthText && card.attacked != null) healthText.text = card.attacked.ToString();
        if (descText) descText.text = card.GetDesc.Invoke();
        if (countDownText) countDownText.text = card.GetComponent<DirectAtkCountdownComponent>().ToString() ?? "";

        var er = card.GetComponent<EnemyRuneComponent>();
        if (er != null)
        {
            runeProgressBar.gameObject.SetActive(er.Activated);
            runeProgressBar.value = er.Progress;
        }
    }
}
