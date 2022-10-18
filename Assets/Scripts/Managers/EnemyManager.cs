using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Card;
using Card.Enemy;
using Visual;

namespace Core
{
    //控制对敌人的可视化和敌人的回合管理
    public class EnemyManager : MonoSingleton<EnemyManager>, IManager
    {
        public List<EnemyCard> enemies = new List<EnemyCard>();
        List<EnemyCard> tombs = new List<EnemyCard>();
        [SerializeField]
        TextAsset enemyData;
        [SerializeField]
        RectTransform enemyBoardTrans;
        [SerializeField]
        GameObject enemyPrefab;
        Dictionary<EnemyCard, GameObject> enemyObjects = new Dictionary<EnemyCard, GameObject>();

        public List<EnemyAction> enemyActions = new List<EnemyAction>();

        new void Awake()
        {
            base.Awake();
            LoadEnemyData();
        }
        public void LoadEnemyData()
        {
            string[] dataRow = enemyData.text.Split('\n');
            var types = GetAllEnemyTypes();
            foreach (var row in dataRow)
            {
                string[] rowArray = row.Split(',');
                if (rowArray.Length < 3 || rowArray[0] == "id")
                {
                    continue;
                }
                string id = rowArray[0];
                string name = rowArray[1];
                int hp = int.Parse(rowArray[2]);
                string[] paras = rowArray.Skip(2).ToArray();
                var ctor = types[id].GetConstructor(new Type[] { typeof(string), typeof(int), typeof(string[]) });
                var enemy = ctor.Invoke(new object[] { name, hp, paras }) as EnemyCard;
                if (enemy == null)
                {
                    Debug.LogError($"创建id={id}，name={name}的敌人失败");
                    continue;
                }
                enemies.Add(enemy);
            }
        }

        public void Refresh()
        {
            foreach (var enemy in enemies)
            {
                if (enemy.battleState == BattleState.Dead)
                {
                    if (enemyObjects.ContainsKey(enemy))
                    {
                        Destroy(enemyObjects[enemy]);
                        enemyObjects.Remove(enemy);
                        enemies.Remove(enemy);
                        tombs.Add(enemy);
                    }
                }
                else
                {
                    if (!enemyObjects.ContainsKey(enemy))
                        enemyObjects[enemy] = CreateEnemyObject(enemy);
                    enemyObjects[enemy].GetComponent<EnemyVisual>().UpdateVisual();
                }
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(enemyBoardTrans);
            if (enemies.Count == 0) GameManager.Instance.GamePass();
        }

        private GameObject CreateEnemyObject(EnemyCard enemy)
        {
            GameObject gameObject = GameObject.Instantiate(enemyPrefab, enemyBoardTrans);
            var enemyDisplay = gameObject.GetComponent<EnemyVisual>();
            enemyDisplay.enemy = enemy;
            return gameObject;
        }

        private static Dictionary<string, Type> GetAllEnemyTypes()
        {
            var res = new Dictionary<string, Type>();
            var type = typeof(EnemyCard);
            var assembly = type.Assembly;
            var assemblyAllTypes = assembly.GetTypes();
            foreach (var t in assemblyAllTypes)
            {
                if (!t.IsAbstract && t.IsSubclassOf(type))
                {
                    res.Add(t.Name, t);
                }
            }
            return res;
        }
        #region IGameTurn 实现部分
        public void GameStart()
        {
            Refresh();
            foreach (var enemy in enemies)
            {
                enemyActions.AddRange(enemy.actions);
            }
        }

        public void PlayerAction()
        {

        }

        public void EnemyAction()
        {
            UpdateActions();
            ExcuteActions();
        }
        void UpdateActions()
        {
            List<EnemyAction> actions = GetTempActionsCopy();
            foreach (var action in actions)
            {
                if (action.enemy.battleState == BattleState.Dead)
                {
                    enemyActions.Remove(action);
                }
            }
        }

        private List<EnemyAction> GetTempActionsCopy()
        {
            List<EnemyAction> actions = new List<EnemyAction>();
            foreach (var action in enemyActions)
                actions.Add(action);
            return actions;
        }

        void ExcuteActions()
        {
            var actions = GetTempActionsCopy();
            foreach (var action in actions)
            {
                action.timer -= 1;
                if (action.timer <= 0)
                {
                    action.action.Invoke();
                    if (action.type == EnemyActionType.Once)
                        enemyActions.Remove(action);
                    else action.ResetTimer();
                }
            }
        }

        public void PlayDraw()
        {

        }
        #endregion
    }
}
