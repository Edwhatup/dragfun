using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Visual
{
    public class PlayerInfoUI : MonoBehaviour
    {
        [SerializeField]
        Text lifeUI;
        // Start is called before the first frame update
        void Start()
        {
            Player.Instance.OnHpChanged += () => lifeUI.text = Player.Instance.hp + "/" + Player.Instance.maxHp;
        }
    }
}
