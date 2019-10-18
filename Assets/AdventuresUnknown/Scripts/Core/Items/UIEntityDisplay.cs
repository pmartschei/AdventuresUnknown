using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.UI.Interfaces;
using AdventuresUnknownSDK.Core.UI.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdventuresUnknown.Scripts.Core.Items
{
    public class UIEntityDisplay : IEntityDisplay
    {

        [SerializeField] private IGameText m_StatText = null;

        #region Properties

        #endregion

        #region Methods
        public override bool Display(Entity entity,string formatter, string[] modTypes)
        {
            if (entity == null)
            {
                if (m_StatText)
                    m_StatText.SetText("");
                return false;
            }
            StringBuilder sb = new StringBuilder();
            if (modTypes == null || modTypes.Length == 0)
            {
                foreach (Stat stat in entity.Stats)
                {
                    string text = stat.ModType.ToText(formatter, stat.Calculated, AdventuresUnknownSDK.Core.Objects.Mods.CalculationType.Calculated);
                    if (text.Length == 0) continue;
                    sb.Append(text);
                    sb.AppendLine();
                }
            }
            else
            {
                foreach(string modType in modTypes)
                {
                    Stat stat = entity.GetStat(modType);
                    string text = stat.ModType.ToText(formatter, stat.Calculated, AdventuresUnknownSDK.Core.Objects.Mods.CalculationType.Calculated);
                    if (text.Length == 0) continue;
                    sb.Append(text);
                    sb.AppendLine();
                }
            }
            if (m_StatText)
                m_StatText.SetText(sb.ToString());
            return true;
        }
        #endregion
    }
}
