﻿using UnityEngine;

namespace Gogos.Managers
{

    public class InputManager : MonoBehaviour
    {
        public static bool LaunchGogoKeyDown => Input.GetKeyDown(KeyCode.Space);
        public static bool ResetGogoKeyDown => Input.GetKeyDown(KeyCode.R);
        public static bool MoveLauncherLeftKey => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        public static bool MoveLauncherRightKey => Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
    }
}