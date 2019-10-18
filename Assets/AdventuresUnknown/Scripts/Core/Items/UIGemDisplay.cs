using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
using AdventuresUnknownSDK.Core.UI.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace AdventuresUnknown.Core.Items
{
    public class UIGemDisplay : UIBasicItemStackDisplay
    {
        [SerializeField] private IGameText m_ImplicitsText = null;
        [SerializeField] private IGameText m_ExplicitsText = null;

        #region Properties

        #endregion

        #region Methods
        public override bool Display(ItemStack itemStack)
        {
            if (!base.Display(itemStack)) return false;

            if (m_ImplicitsText)
            {
                StringBuilder sb = new StringBuilder();
                string[] implicits = itemStack.GetImplicitTexts();
                for (int i = 0; i < implicits.Length; i++)
                {
                    if (i == implicits.Length - 1)
                        sb.Append(implicits[i]);
                    else
                        sb.AppendLine(implicits[i]);
                }
                m_ImplicitsText.SetText(sb.ToString());
            }
            if (m_ExplicitsText)
            {
                StringBuilder sb = new StringBuilder();
                string[] explicits = itemStack.GetExplicitTexts();
                for (int i = 0; i < explicits.Length; i++)
                {
                    if (i == explicits.Length - 1)
                        sb.Append(explicits[i]);
                    else
                        sb.AppendLine(explicits[i]);
                }
                m_ExplicitsText.SetText(sb.ToString());
            }

            return true;
        }
        #endregion

    }
}
