using Gogos;
using UnityEditor;
using UnityEngine;

namespace GogosEditor
{
    public class TimeScaleWindow : EditorWindow
    {
        private float m_MinTimeScale = 0;
        private float m_MaxTimeScale = 10;
        private bool m_ChangeByPhase = true;

        [MenuItem("Gogos/Window/Time Scale")]
        public static void ShowWindow()
        {
            GetWindow<TimeScaleWindow>(false, "Time Scale");
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged -= EditorApplication_OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += EditorApplication_OnPlayModeStateChanged;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= EditorApplication_OnPlayModeStateChanged;
        }

        private void OnGUI()
        {
            Time.timeScale = EditorGUILayout.Slider("Time Scale", Time.timeScale, m_MinTimeScale, m_MaxTimeScale);
            m_MinTimeScale = EditorGUILayout.FloatField("Min Time Scale", m_MinTimeScale);
            m_MaxTimeScale = EditorGUILayout.FloatField("Max Time Scale", m_MaxTimeScale);
            m_ChangeByPhase = EditorGUILayout.Toggle("Change By Phase", m_ChangeByPhase);
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

        private void EditorApplication_OnPlayModeStateChanged(PlayModeStateChange playModeStateChange)
        {
            if (!m_ChangeByPhase)
            {
                return;
            }

            if (playModeStateChange == PlayModeStateChange.EnteredPlayMode)
            {
                PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;
                Time.timeScale = m_MaxTimeScale;
            }
            else if (playModeStateChange == PlayModeStateChange.ExitingPlayMode)
            {
                PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
            }
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.Selecting)
            {
                Time.timeScale = 1;
            }
        }
    }
}
