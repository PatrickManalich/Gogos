using UnityEngine;

namespace Gogos.Managers
{

    public class InputManager : MonoBehaviour
    {
        public static bool LaunchGogoKeyDown => Input.GetKeyDown(KeyCode.Space);
        public static bool ResetGogoKeyDown => Input.GetKeyDown(KeyCode.R);
        public static bool MoveLauncherLeftKey => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        public static bool MoveLauncherRightKey => Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        public static bool IncreaseLaunchForceKey => Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        public static bool DecreaseLaunchForceKey => Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    }
}