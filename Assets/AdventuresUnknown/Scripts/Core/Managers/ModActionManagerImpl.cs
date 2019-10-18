using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Mods;
using AdventuresUnknownSDK.Core.Objects.Mods.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.AdventuresUnknown.Scripts.Core.Managers
{
    public class ModActionManagerImpl : ModActionManager
    {
        private Dictionary<int, List<BaseAction>> m_ModActionDictionary = new Dictionary<int, List<BaseAction>>();

        #region Properties

        #endregion

        #region Methods
        private void Start()
        {
            ModType[] modTypes = ObjectsManager.GetAllObjects<ModType>();
            foreach (ModType modType in modTypes)
            {
                ModActionCollection modActionCollection = modType.ModActionCollection;
                if (!modActionCollection) continue;
                modActionCollection.Initialize(modType);

                BaseAction[] baseActions = modActionCollection.BaseActions;
                
                foreach(BaseAction baseAction in baseActions)
                {
                    if (!baseAction) continue;
                    if (!baseAction.ActionType) continue;
                    List<BaseAction> modActionList = null;

                    if (!m_ModActionDictionary.TryGetValue(baseAction.ActionType.Value,out modActionList))
                    {
                        modActionList = new List<BaseAction>();
                        m_ModActionDictionary.Add(baseAction.ActionType.Value, modActionList);
                    }
                    modActionList.Add(baseAction);
                }
            }
            SortModDictionaries();
        }
        private void SortModDictionaries()
        {
            foreach (List<BaseAction> list in m_ModActionDictionary.Values)
            {
                list.Sort((first, second) => { return second.Priority - first.Priority; });
            }
        }

        protected override List<BaseAction> GetActionsImpl(ActionType actionType, params ModType[] modTypes)
        {
            List<BaseAction> filteredBaseActions = new List<BaseAction>();
            if (actionType == null) return filteredBaseActions;

            filteredBaseActions = GetActionsImpl(actionType.Value, modTypes);

            return filteredBaseActions;
        }

        protected override List<BaseAction> GetActionsImpl(int actionValue, params ModType[] modTypes)
        {
            List<BaseAction> filteredBaseActions = new List<BaseAction>();
            List<BaseAction> allBaseActions = new List<BaseAction>();

            if (m_ModActionDictionary.TryGetValue(actionValue, out allBaseActions))
            {
                if (modTypes == null)
                {
                    filteredBaseActions.AddRange(allBaseActions);
                }
                else
                {
                    filteredBaseActions.AddRange(allBaseActions.FindAll((baseAction) => { return modTypes.Contains(baseAction.ModType); }));
                }
            }

            return filteredBaseActions;
        }

        protected override List<int> GetAllRegisteredActionValuesImpl()
        {
            return m_ModActionDictionary.Keys.ToList();
        }
        #endregion
    }
}
