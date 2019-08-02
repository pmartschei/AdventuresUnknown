#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace AdventuresUnknown.Core.Levels
{
    [CustomEditor(typeof(LevelController))]
    public class LevelControllerEditor : Editor
    {


        #region Properties

        #endregion

        #region Methods
        public override void OnInspectorGUI()
        {
            LevelController levelController = this.target as LevelController;
            base.OnInspectorGUI();
            if (levelController == null) return;
            if (GUILayout.Button("Regenerate Borders"))
            {
                levelController.GenerateLevelBorders();
            }
        }
        #endregion
    }
}
#endif