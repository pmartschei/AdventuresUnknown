using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Mods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static AdventuresUnknownSDK.Core.Objects.Tags.WeightedTagList;

namespace AdventuresUnknown.Core.Managers
{
    public class ModifierManagerImpl : ModifierManager
    {

        private Dictionary<int, Dictionary<string, List<Mod>>> m_ModDictionary = new Dictionary<int, Dictionary<string, List<Mod>>>();

        #region Properties

        #endregion

        #region Methods

        public void Init()
        {
            Mod[] mods = ObjectsManager.GetAllObjects<Mod>();
            foreach (Mod mod in mods)
            {
                Dictionary<string, List<Mod>> tagDictionary = new Dictionary<string, List<Mod>>();
                if (m_ModDictionary.ContainsKey(mod.Domain))
                {
                    tagDictionary = m_ModDictionary[mod.Domain];
                }
                else
                {
                    m_ModDictionary.Add(mod.Domain, tagDictionary);
                }
                foreach (WeightedTag wt in mod.WeightedTagList)
                {
                    if (wt.Weight <= 0) continue;
                    List<Mod> modList = new List<Mod>();
                    if (tagDictionary.ContainsKey(wt.Tag.Identifier))
                    {
                        modList = tagDictionary[wt.Tag.Identifier];
                    }
                    else
                    {
                        tagDictionary.Add(wt.Tag.Identifier, modList);
                    }
                    modList.Add(mod);
                }
            }
        }
        protected override Mod[] GetModifiersForDomainAndTagImpl(int domain, params string[] tags)
        {
            if (!m_ModDictionary.ContainsKey(domain)) return null;
            Dictionary<string, List<Mod>> tagDictionary = m_ModDictionary[domain];
            List<Mod> availableMods = new List<Mod>();
            foreach (var entry in tagDictionary)
            {
                if (tags == null || tags.Contains(entry.Key))
                    availableMods.AddRange(entry.Value);
            }
            return availableMods.Distinct().ToArray();
        }

        protected override Mod[] GetModifiersForDomainImpl(int domain)
        {
            return GetModifiersForDomainAndTag(domain, null);
        }
        #endregion
    }
}
