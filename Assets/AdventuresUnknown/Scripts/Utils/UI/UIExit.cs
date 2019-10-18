using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventuresUnknown.Utils.UI
{
    public class UIExit : MonoBehaviour
    {
        public void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

}