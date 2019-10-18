using AdventuresUnknownSDK.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdventuresUnknown.Scripts.Core.Managers
{
    public class LevelObjectManagerImpl : LevelObjectManager
    {
        [SerializeField] private GameObject m_WallPrefab = null;
        protected override GameObject WallImpl => m_WallPrefab;
    }
}
