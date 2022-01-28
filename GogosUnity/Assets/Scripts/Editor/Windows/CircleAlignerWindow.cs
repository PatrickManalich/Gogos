using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GogosEditor
{
    public class CircleAlignerWindow : EditorWindow
    {
        private int m_SelectedObjectsCount;
        private float m_Radius;

        [MenuItem("Gogos/Window/Circle Aligner")]
        public static void ShowWindow()
        {
            GetWindow<CircleAlignerWindow>(false, "Circle Aligner");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Selected Objects Count: ", m_SelectedObjectsCount.ToString());
            EditorGUILayout.Space();

            m_Radius = EditorGUILayout.FloatField("Radius", m_Radius);
            if (GUILayout.Button("Align"))
            {
                var diagonal = Mathf.Ceil(Mathf.Sqrt(m_Radius * m_Radius / 2));

                ((GameObject)Selection.objects[0]).transform.localPosition = new Vector3(0, 0, m_Radius);
                ((GameObject)Selection.objects[1]).transform.localPosition = new Vector3(diagonal, 0, diagonal);
                ((GameObject)Selection.objects[2]).transform.localPosition = new Vector3(m_Radius, 0, 0);
                ((GameObject)Selection.objects[3]).transform.localPosition = new Vector3(diagonal, 0, -diagonal);
                ((GameObject)Selection.objects[4]).transform.localPosition = new Vector3(0, 0, -m_Radius);
                ((GameObject)Selection.objects[5]).transform.localPosition = new Vector3(-diagonal, 0, -diagonal);
                ((GameObject)Selection.objects[6]).transform.localPosition = new Vector3(-m_Radius, 0, 0);
                ((GameObject)Selection.objects[7]).transform.localPosition = new Vector3(-diagonal, 0, diagonal);

                EditorSceneManager.MarkAllScenesDirty();
            }
        }

        private void OnSelectionChange()
        {
            m_SelectedObjectsCount = Selection.objects == null ? 0 : Selection.objects.Length;
            Repaint();
        }
    }
}
