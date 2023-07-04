using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPack : MonoBehaviour
{
    [SerializeField] private bool initOnStart = true;
    [SerializeField] private Transform cardParent;
    [SerializeField] private GameObject cardEntry;

    public List<CardEntry> Entries => entries;
    private List<CardEntry> entries = new List<CardEntry>();

    private void Start()
    {
        if (initOnStart) UpdatePack();
    }

    public void UpdatePack()
    {
        Clear();
        foreach (var item in Player.Instance.deck)
        {
            for (int i = 0; i < item.Value; i++)
            {
                var c = CardStore.Instance.CreateCard(item.Key, false);
                var v = Instantiate(cardEntry, Vector3.zero, Quaternion.identity, cardParent).GetComponent<CardEntry>();
                v.BindCard(c);
                entries.Add(v);
            }
            // Debug.Log("gg");
        }
    }

    public void Clear()
    {
        entries.Clear();
        for (int i = 0; i < cardParent.childCount; i++) Destroy(cardParent.GetChild(i).gameObject);
    }
}
