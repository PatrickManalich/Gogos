using System;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GogosEditor
{
    public class RenameWindow : EditorWindow
    {
        private int m_SelectedObjectsCount;
        private string m_SearchTerm;
        private string m_ReplacementTerm;
        private string m_PrefixTerm;
        private string m_PostfixTerm;
        private int m_StartNumber;
        private int m_CurrentIncrement;

        [MenuItem("Gogos/Window/Rename")]
        public static void ShowWindow()
        {
            GetWindow<RenameWindow>(false, "Rename");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Selected Objects Count: ", m_SelectedObjectsCount.ToString());
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Replace", EditorStyles.boldLabel);
            m_SearchTerm = EditorGUILayout.TextField("Search Term: ", m_SearchTerm);
            m_ReplacementTerm = EditorGUILayout.TextField("Replacement Term: ", m_ReplacementTerm);
            if (GUILayout.Button("Replace"))
            {
                RenameSelectedObjects(s => s.Replace(m_SearchTerm, m_ReplacementTerm));
            }
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Prepend", EditorStyles.boldLabel);
            m_PrefixTerm = EditorGUILayout.TextField("Prefix Term: ", m_PrefixTerm);
            if (GUILayout.Button("Prepend"))
            {
                RenameSelectedObjects(s => m_PrefixTerm + s);
            }
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Append", EditorStyles.boldLabel);
            m_PostfixTerm = EditorGUILayout.TextField("Postfix Term: ", m_PostfixTerm);
            if (GUILayout.Button("Append"))
            {
                RenameSelectedObjects(s => s + m_PostfixTerm);
            }
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Append Increment", EditorStyles.boldLabel);
            m_StartNumber = EditorGUILayout.IntField("Start Number: ", m_StartNumber);
            if (GUILayout.Button("Append Increment"))
            {
                m_CurrentIncrement = m_StartNumber;
                // Removes Unity increment if present, e.g. GameObject (1)
                RenameSelectedObjects(s => Regex.Replace(s, @"[\d\s()]", string.Empty) + m_CurrentIncrement++);
            }
            EditorGUILayout.Space();
        }

        private void OnSelectionChange()
        {
            m_SelectedObjectsCount = Selection.objects == null ? 0 : Selection.objects.Length;
            Repaint();
        }

        private void RenameSelectedObjects(Func<string, string> rename)
        {
            var renamedCount = 0;
            for (int i = 0; i < Selection.objects.Length; i++)
            {
                var obj = Selection.objects[i];
                var oldName = obj.name;
                var newName = rename(oldName);

                if (oldName != newName)
                {
                    var assetPath = AssetDatabase.GetAssetPath(obj);
                    if (assetPath != string.Empty)   // Selected object is an asset
                    {
                        AssetDatabase.RenameAsset(assetPath, newName);
                        assetPath = AssetDatabase.GetAssetPath(obj);    // Name has changed, so path has too
                        AssetDatabase.ForceReserializeAssets(new string[] { assetPath });
                    }
                    else   // Selected object is a game object in an open scene
                    {
                        obj.name = newName;
                        EditorSceneManager.MarkAllScenesDirty();
                    }
                    renamedCount++;
                }
            }
            Debug.Log($"{renamedCount}/{m_SelectedObjectsCount} object(s) renamed");
        }
    }
}
