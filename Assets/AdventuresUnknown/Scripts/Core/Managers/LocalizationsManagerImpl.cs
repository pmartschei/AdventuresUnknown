using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AdventuresUnknown.Core.Managers
{
    public class LocalizationsManagerImpl : LocalizationsManager
    {

        private Hashtable m_LanguagesHashtable = new Hashtable();

        #region Properties

        #endregion

        #region Methods
        public void Init()
        {
            LoadCoreLocalizationTables();
        }

        public void LoadLocalizationTables(LocalizationTable[] localizationTables)
        {
            if (localizationTables == null || localizationTables.Length == 0) return;

            List<LocalizationTable> validLocalizationTables = new List<LocalizationTable>();
            foreach(LocalizationTable localizationTable in localizationTables)
            {
                if (!localizationTable.ConsistencyCheck())
                {
                    //Debug.LogErrorFormat("Skipped LocalizationTable : {0}", AssetDatabase.GetAssetPath(localizationTable));
                    continue;
                }
                validLocalizationTables.Add(localizationTable);
            }

            foreach(LocalizationTable localizationTable in validLocalizationTables)
            {
                Hashtable hashtable = GetHashtableOrAddNew(localizationTable.Language);

                LocalizationTable.IdentifierString[] datas = localizationTable.LocalizationData;

                foreach(LocalizationTable.IdentifierString data in datas)
                {
                    if (hashtable.ContainsKey(data.Identifier))
                    {
                        hashtable[data] = data.String;
                    }
                    else
                    {
                        hashtable.Add(data.Identifier, data.String);
                    }
                }
                GameConsole.LogFormat("Added or changed {0} localization entries (Language = {1})", datas.Length, localizationTable.Language.Identifier);
            }
        }
        private void LoadCoreLocalizationTables()
        {
            LocalizationTable[] localizationTables = Resources.LoadAll<LocalizationTable>("");
            LoadLocalizationTables(localizationTables);
        }

        private Hashtable GetHashtableOrAddNew(LocalizationLanguage language)
        {
            Hashtable found = new Hashtable();

            if (m_LanguagesHashtable.ContainsKey(language))
            {
                found = m_LanguagesHashtable[language] as Hashtable;
            }
            else
            {
                m_LanguagesHashtable.Add(language, found);
            }

            return found;
        }

        protected override string LocalizeImpl(string identifier)
        {
            return LocalizeImpl(identifier, GameSettingsManager.Language);
        }

        protected override string LocalizeImpl(string identifier, LocalizationLanguage language)
        {
            string nullString = string.Format("(({0}))", identifier);
            if (language == null) return nullString;
            Hashtable hashtable = GetHashtableOrAddNew(language);
            if (!hashtable.ContainsKey(identifier)) return nullString;
            return hashtable[identifier] as string;
        }
        #endregion
    }
}
