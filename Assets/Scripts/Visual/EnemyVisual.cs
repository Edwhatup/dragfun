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
        if (healthText && card.attacked!=null) healthText.text = card.attacked.ToString();
        if (descText) descText.text = card.GetDesc.Invoke();
    }
}
