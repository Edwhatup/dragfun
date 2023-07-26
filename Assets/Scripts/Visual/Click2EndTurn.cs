using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click2EndTurn : MonoBehaviour
{
    public void EndTurn()
    {
        TurnManager.Instance.NextTurn();
    }
}
