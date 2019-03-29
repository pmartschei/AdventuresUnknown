using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace AdventuresUnknown.Core.Items
{
    public class UIEquipmentDisplay : UIBasicItemStackDisplay
    {

        [SerializeField] private TMPro.TMP_InputField m_ImplicitsText = null;

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
                m_ImplicitsText.text = sb.ToString();
            }

            return true;
        }
        #endregion

    }
}
