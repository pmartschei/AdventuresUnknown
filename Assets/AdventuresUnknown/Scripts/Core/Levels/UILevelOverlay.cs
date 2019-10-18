using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Levels;
using AdventuresUnknownSDK.Core.Objects.Mods;
using AdventuresUnknownSDK.Core.Objects.Mods.ModBases;
using AdventuresUnknownSDK.Core.Objects.Tags;
using AdventuresUnknownSDK.Core.UI.Interfaces;
using AdventuresUnknownSDK.Core.UI.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static AdventuresUnknownSDK.Core.Objects.Inventories.ItemStack;

namespace AdventuresUnknown.Core.Levels
{
    public class UILevelOverlay : AbstractLevelDisplay
    {
        [SerializeField] private IGameText m_LevelNameText = null;
        [SerializeField] private IGameText m_WavesText = null;
        [SerializeField] private IGameText m_DelayText = null;
        [SerializeField] private IGameText m_TagsText = null;
        [SerializeField] private IGameText m_EnemyLevelText = null;
        [SerializeField] private IGameText m_AttributesText = null;

        private Level m_CurrentLevel = null;

        private void OnEnable()
        {
            GameSettingsManager.OnLanguageChange.AddListener(OnLanguageChange);
            OnLanguageChange();
        }

        private void OnDisable()
        {
            GameSettingsManager.OnLanguageChange.RemoveListener(OnLanguageChange);
        }
        public override bool Display(Level level)
        {
            m_CurrentLevel = level;
            if (m_CurrentLevel == null) return false;
            if (m_LevelNameText)
                m_LevelNameText.SetText(level.LevelPrefix.LocalizedString + " " + level.LevelName.LocalizedString + " " + level.LevelSuffix.LocalizedString);
            if (m_WavesText)
                m_WavesText.SetText(level.WaveCount);
            if (m_DelayText)
                m_DelayText.SetText(level.TimeBetweenWaves);
            if (m_TagsText)
            {
                StringBuilder sb = new StringBuilder();

                foreach(Tag tag in level.TagList)
                {
                    sb.Append("<color=#");
                    sb.Append(tag.HTMLColor);
                    sb.Append(">");
                    sb.Append(tag.TagName);
                    sb.Append("</color> ");
                }

                m_TagsText.SetText(sb.ToString());
            }
            if (m_EnemyLevelText)
            {
                m_EnemyLevelText.SetText(level.EnemyLevel);
            }
            if (m_AttributesText)
            {
                StringBuilder sb = new StringBuilder();
                Entity entity = new Entity();
                HashSet<BasicModBase> hashAttributes = new HashSet<BasicModBase>();
                foreach (ValueMod vm in level.Attributes)
                {
                    entity.GetStat(vm.BasicModBase.ModTypeIdentifier).AddStatModifier(new StatModifier(vm.Value, vm.BasicModBase.CalculationType, level));
                    if (hashAttributes.Contains(vm.BasicModBase)) continue;
                    hashAttributes.Add(vm.BasicModBase);
                }
                foreach(BasicModBase bmb in hashAttributes)
                {
                    Stat stat = entity.GetStat(bmb.ModTypeIdentifier);
                    sb.Append(stat.ModType.ToText("default", stat.GetValue(bmb.CalculationType), bmb.CalculationType));
                    sb.AppendLine();
                }
                m_AttributesText.SetText(sb.ToString());
            }
            
            return true;
        }

        private void OnLanguageChange()
        {
            Display(m_CurrentLevel);
        }
    }

}