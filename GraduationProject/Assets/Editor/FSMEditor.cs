﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class FSMEditor : EditorWindow
{
    FSMEditor()

    {

        this.titleContent = new GUIContent("测试编辑器窗口");

    }

    //2.这是在哪里创建窗口

    [MenuItem("DreamerEditor/FSM脚本模板")]

    static void CreateTestWindows()

    {

        EditorWindow.GetWindow(typeof(FSMEditor));

    }
    private void OnEnable()

    {



    }
    string script_name;

    MonoScript script;
    private void OnGUI()
    {
        EditorGUILayout.LabelField("姓名");
        script_name = EditorGUILayout.TextField(script_name);
        EditorGUILayout.LabelField("拥有者");
        script = (MonoScript)EditorGUILayout.ObjectField(script, typeof(MonoScript), false);
        if (GUILayout.Button("生成FSM状态脚本", GUILayout.Height(30)))
        {
            using (FileStream f = new FileStream("Assets/" + script_name + ".cs", FileMode.CreateNew))
            {
                using (StreamWriter write = new StreamWriter(f))
                {
                    write.WriteLine(@"
using DreamerTool.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class " + script_name + @" : StateBaseTemplate<" + script.name + @">
{
    public " + script_name + @"(string _id," + script.name + @" owner):base(_id,owner)
    {
        this.id = _id;
        this.owner  = owner;
    }

"
+@" public override void OnEnter(params object[] args)
    {

    }"
+ @" 
    public override void OnExit(params object[] args)
    {

    }"
+ @" 
    public override void OnStay(params object[] args)
    {

    }
}"
        );

                }
            }
            AssetDatabase.Refresh();

        }
    }
}