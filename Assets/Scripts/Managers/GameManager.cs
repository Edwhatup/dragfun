using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using Seletion;

namespace Core
{
    public enum GamePhase
    {
        GameStart, PlayerAction, EnemyAction
    }

    public class GameManager : MonoSingleton<GameManager>, IManager
    {
        List<IManager> managers = new List<IManager>();
        //初始化阶段
        public GamePhase GamePhase = GamePhase.GameStart;
        public event Action<GamePhase, GamePhase> phaseChangeEvent;
        // Start is called before the first frame update
        void Start()
        {
            managers.Add(EnemyManager.Instance);
            managers.Add(CardManager.Instance);
        }



        #region IGameTurn 实现
        public void GameStart()
        {
            SwicthGamePhase(GamePhase.GameStart);
            foreach (var i in managers)
                i.GameStart();
            PlayerAction();
        }

        public void PlayerAction()
        {
            SwicthGamePhase(GamePhase.PlayerAction);
            foreach (var i in managers)
                i.PlayerAction();
        }

        public void EnemyAction()
        {
            SwicthGamePhase(GamePhase.EnemyAction);
            foreach (var i in managers)
                i.EnemyAction();
        }
        private void SwicthGamePhase(GamePhase nextPhase)
        {
            phaseChangeEvent?.Invoke(GamePhase, nextPhase);
            GamePhase = nextPhase;
        }

        public void Refresh()
        {
            foreach (var i in managers)
                i.Refresh();
        }
        #endregion

        #region 回合管理相关
        public void Click2EnemtAction()
        {
            EnemyAction();
        }
        public void Click2GameStart()
        { 
            GameStart();
        }
        public void GameFalse()
        {
            Console.WriteLine("游戏失败");
        }

        public void GamePass()
        {
            Console.WriteLine("游戏通关");
        }


        #endregion
    }
}
