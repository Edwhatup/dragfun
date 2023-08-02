using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using static UnityEditor.EditorGUILayout;

[CustomEditor(typeof(CardLibDisplayer))]
public class CardLibDisplayerDrawer : Editor
{
    private List<Card> cache = null;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        BeginHorizontal();
        LabelField("名称", GUILayout.Width(100));
        LabelField("类型", GUILayout.Width(100));
        LabelField("血量", GUILayout.Width(50));
        LabelField("攻击", GUILayout.Width(50));
        EndHorizontal();
        Space();

        if (cache == null)
        {
            var c = typeof(Card);
            cache = c.Assembly.GetTypes().Where(i => i.IsSubclassOf(c) && i != typeof(Player)).Select(i => (Card)Activator.CreateInstance(i, new object[] { null })).Where(i => i.type != CardType.Enemy && i.type != CardType.EnemyDerive).ToList();
        }
        foreach (var item in cache)
        {
            BeginHorizontal();
            LabelField(item.name, GUILayout.Width(100));
            LabelField(item.type + "", GUILayout.Width(100));
            LabelField(item.attacked != null ? item.attacked.hp + "" : "/", GUILayout.Width(50));
            LabelField(item.attack != null ? item.attack.initAtk + "" : "/", GUILayout.Width(50));
            EndHorizontal();
        }
    }
}