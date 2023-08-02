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
        LabelField("类名", GUILayout.Width(120));
        LabelField("名称", GUILayout.Width(100));
        LabelField("类型", GUILayout.Width(100));
        LabelField("血量", GUILayout.Width(50));
        EndHorizontal();
        Space();

        if (cache == null)
        {
            var c = typeof(Card);
            cache = c.Assembly.GetTypes().Where(i => i.IsSubclassOf(c) && i != typeof(Player)).Select(i => (Card)Activator.CreateInstance(i, new object[] { null })).ToList();
        }
        foreach (var item in cache)
        {
            BeginHorizontal();
            LabelField(item.GetType().ToString(), GUILayout.Width(120));
            LabelField(item.name, GUILayout.Width(100));
            LabelField(item.type + "", GUILayout.Width(100));
            LabelField(item.attacked != null ? item.attacked.hp + "" : "/", GUILayout.Width(50));
            EndHorizontal();
        }
    }
}