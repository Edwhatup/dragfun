using System;
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

    private List<Func<Card, bool>> selectConditions;
    private List<Card> selectedCards;
    private Action<List<Card>> selectCbk;
    private Card selector;
    private bool allowRepeat, includeSelf;

    public bool Selecting => selecting;
    private bool selecting = false;

    private void Start()
    {
        if (initOnStart) UpdatePack();
    }

    public void UpdatePack()
    {
        Clear();
        Player.Instance.CardDeck.ForEach(c =>
        {
            var v = Instantiate(cardEntry, Vector3.zero, Quaternion.identity, cardParent).GetComponent<CardEntry>();
            v.BindCard(c);
            entries.Add(v);
        });
    }

    public void Clear()
    {
        EndSelecting();

        entries.Clear();
        for (int i = 0; i < cardParent.childCount; i++) Destroy(cardParent.GetChild(i).gameObject);
    }

    public void Select(Card source, List<Func<Card, bool>> conds, Action<List<Card>> cbk, bool allowRepeat = false, bool includeSelf = false)
    {
        selectConditions = conds;
        selectedCards = new List<Card>();
        selectCbk = cbk;
        this.allowRepeat = allowRepeat;
        this.includeSelf = includeSelf;
        selecting = true;

        entries.ForEach(i => i.Clicked += OnCardSelected);
        if (UpdateSelectableVisual() == 0)
        {
            EndSelecting(false);
            Debug.LogWarning("没有牌可以选择!");
        }
    }

    private void OnCardSelected(CardEntry e) => OnCardSelected(e.Card);

    private void OnCardSelected(Card c)
    {
        if (!selecting) return;
        if (IsSelectable(c))
        {
            selectedCards.Add(c);
            if (selectedCards.Count == selectConditions.Count)
            {
                EndSelecting();
                return;
            }

            var cnt = UpdateSelectableVisual();
            if (cnt == 0)   // 虽然没有选完，但是已经没有牌可以选了
                EndSelecting();
        }
    }

    private int UpdateSelectableVisual()
    {
        var cnt = 0;
        if (!selecting) return cnt;
        entries.ForEach(i =>
        {
            var whether = IsSelectable(i.Card);
            i.SetSelectableState(whether);
            if (whether) cnt++;
        });
        return cnt;
    }

    private bool IsSelectable(Card c)
        => selectConditions[selectedCards.Count].Invoke(c)
            && (!selectedCards.Contains(c) || allowRepeat)
            && (includeSelf || c != selector);

    private void EndSelecting(bool runCbk = true)
    {
        if (!selecting) return;
        selecting = false;
        entries.ForEach(i => i.Clicked -= OnCardSelected);
        selectConditions = null;
        selector = null;
        allowRepeat = false;
        includeSelf = false;

        if (runCbk) selectCbk.Invoke(selectedCards);
    }
}
