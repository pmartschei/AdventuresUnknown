using AdventuresUnknownSDK.Core.Objects.Items;
using AdventuresUnknownSDK.Core.UI.Items;
using AdventuresUnknownSDK.Core.UI.Tooltip;
using AdventuresUnknownSDK.Core.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdventuresUnknown.Scripts.Core.Logic.Play
{
    public class ActiveGemDataTooltip : Tooltip<ActiveGemData>
    {
        [SerializeField] private AbstractActiveGemDisplay m_DisplayPrefab = null;

        private ActiveGem m_CurrentDisplayActiveGem = null;
        private AbstractActiveGemDisplay m_AbstractActiveGemDisplay = null;
        public override void Display(ActiveGemData data)
        {
            if (data == null) return;
            if (data.ActiveGem != m_CurrentDisplayActiveGem)
            {
                VisibleTransform.Clear();
                m_CurrentDisplayActiveGem = data.ActiveGem;
                if (data.ActiveGem.DisplayPrefab == null)
                {
                    m_AbstractActiveGemDisplay = Instantiate(m_DisplayPrefab, VisibleTransform);
                }
                else
                {
                    m_AbstractActiveGemDisplay = Instantiate(data.ActiveGem.DisplayPrefab, VisibleTransform);
                }
            }
            m_AbstractActiveGemDisplay?.Display(data.ActiveGem, data.Entity, data.ModTypes);
        }
    }
}
