using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpecialSkillsEffects
{
    public class LookAtTarget : MonoBehaviour
    {
        public Transform Target;

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(Target);
        }
    }
}