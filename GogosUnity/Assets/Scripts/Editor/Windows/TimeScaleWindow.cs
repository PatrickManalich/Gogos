using UnityEditor;
using UnityEngine;

namespace GogosEditor
{
    public class TimeScaleWindow : EditorWindow
    {
        private float m_MinTimeScale = 0;
        private float m_MaxTimeScale = 2;

        [MenuItem("Gogos/Window/Time Scale")]
        public static void ShowWindow()
        {
            GetWindow<TimeScaleWindow>(false, "Time Scale");
        }

        private void OnGUI()
        {
            Time.timeScale = EditorGUILayout.Slider("Time Scale", Time.timeScale, m_MinTimeScale, m_MaxTimeScale);
            m_MinTimeScale = EditorGUILayout.FloatField("Min Time Scale", m_MinTimeScale);
            m_MaxTimeScale = EditorGUILayout.FloatField("Max Time Scale", m_MaxTimeScale);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Normal"))
            {
                Time.timeScale = 1;
            }
            if (GUILayout.Button("Min Speed"))
            {
                Time.timeScale = m_MinTimeScale;
            }
            if (GUILayout.Button("Max Speed"))
            {
                Time.timeScale = m_MaxTimeScale;
            }
            GUILayout.EndHorizontal();
        }
    }
}
