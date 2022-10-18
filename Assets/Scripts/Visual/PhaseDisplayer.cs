using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Visual {

    public class PhaseDisplayer : MonoBehaviour
    {
        public Text phaseText;

        void Start()
        {
            GameManager.Instance.phaseChangeEvent += (arg1, arg2) => UpdateText(arg2);
        }

        void UpdateText(GamePhase current)
        {
            switch (current)
            {
                case GamePhase.GameStart:
                    phaseText.text = "当前回合:" + "游戏开始";
                    break;
                case GamePhase.PlayerDraw:
                    phaseText.text = "当前回合:" + "玩家抽牌";
                    break;
                case GamePhase.PlayerAction:
                    phaseText.text = "当前回合:" + "玩家行动";
                    break;
                case GamePhase.EnemyAction:
                    phaseText.text = "当前回合:" + "敌人行动";
                    break;

            }
        }
    }

}
