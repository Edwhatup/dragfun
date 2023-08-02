using System.Collections;
using UnityEngine;

public class CardList : MonoBehaviour
{
    public Transform cardParent;

    private bool inited = false;

    private void Start()
    {
        if (inited) return;
        inited = true;
        foreach (var i in CardStore.cardNameList)
        {
            var c = CardStore.Instance.CreateCard(new CardInfo() { name = i });
            if (c.type != CardType.Enemy && c.type != CardType.EnemyDerive) c.visual.transform.SetParent(cardParent);
            else Destroy(c.visual.gameObject);
        }
    }
}