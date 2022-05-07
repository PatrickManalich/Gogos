using UnityEngine;

namespace Gogos
{
    public static class InputKeys
    {
        public static class User
        {
            public static bool ResetGogoKeyDown => Input.GetKeyDown(KeyCode.R);
            public static bool TurnLauncherLeftKey => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
            public static bool TurnLauncherRightKey => Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
            public static bool IncreaseLaunchForceKey => Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
            public static bool DecreaseLaunchForceKey => Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
            public static bool CyclePreviousLaunchPointKey => Input.GetKeyDown(KeyCode.Q) || (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Tab));
            public static bool CycleNextLaunchPointKey => Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Tab);
            public static bool LaunchGogoKeyDown => Input.GetKeyDown(KeyCode.Space);
            public static bool SubmitKeyDown => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return);
            public static bool SubmitKeyUp => Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return);
        }

        public static class Dev
        {
            public static bool IsInDevMode => Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.LeftShift);
            public static bool SwitchObjectivesKeyDown => IsInDevMode && Input.GetKeyDown(KeyCode.O);
        }
    }
}
