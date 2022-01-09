using UnityEditor;
using UnityEngine;

namespace BarronAssociatesEditor
{
    public class TimeScaleWindow : EditorWindow
    {
        private const int DefaultMinTimeScale = 0;
        private const int DefaultMaxTimeScale = 2;

        private float m_MinTimeScale = DefaultMinTimeScale;
        private float m_MaxTimeScale = DefaultMaxTimeScale;

        [MenuItem("Gogos/Window/Time Scale Window")]
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
            if (GUILayout.Button("Default"))
            {
                m_MinTimeScale = DefaultMinTimeScale;
                m_MaxTimeScale = DefaultMaxTimeScale;
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
