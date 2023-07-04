using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPack : MonoBehaviour
{
    [SerializeField] private Transform cardParent;

    private void Start()
    {
        foreach (var item in Player.Instance.deck)
        {
            
        }
    }
}
