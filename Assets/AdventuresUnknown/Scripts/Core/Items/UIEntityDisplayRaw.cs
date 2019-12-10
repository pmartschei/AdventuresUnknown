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
    public class UIEntityDisplayRaw : IEntityDisplay
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
                    AppendStat(formatter, stat, sb);
                }
            }
            else
            {
                foreach(string modType in modTypes)
                {
                    Stat stat = entity.GetStat(modType);
                    AppendStat(formatter, stat, sb);
                }
            }
            if (m_StatText)
                m_StatText.SetText(sb.ToString());
            return true;
        }

        public void AppendStat(string formatter, Stat stat, StringBuilder sb)
        {
            string text = stat.ModType.ToText(formatter, stat.Flat, AdventuresUnknownSDK.Core.Objects.Mods.CalculationType.Flat);
            if (text.Length != 0)
            {
                sb.Append(text);
                sb.AppendLine();
            }
            text = stat.ModType.ToText(formatter, stat.Increased - 1.0f, AdventuresUnknownSDK.Core.Objects.Mods.CalculationType.Increased);
            if (text.Length != 0)
            {
                sb.Append(text);
                sb.AppendLine();
            }
            text = stat.ModType.ToText(formatter, stat.More - 1.0f, AdventuresUnknownSDK.Core.Objects.Mods.CalculationType.More);
            if (text.Length != 0)
            {
                sb.Append(text);
                sb.AppendLine();
            }
            text = stat.ModType.ToText(formatter, stat.FlatExtra, AdventuresUnknownSDK.Core.Objects.Mods.CalculationType.FlatExtra);
            if (text.Length != 0)
            {
                sb.Append(text);
                sb.AppendLine();
            }
        }
        #endregion
    }
}
