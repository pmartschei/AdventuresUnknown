using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.DropTables;
using AdventuresUnknownSDK.Core.Objects.Enemies;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Utils.Identifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdventuresUnknown.Scripts.Core.Managers
{
    public class DropManagerImpl : DropManager
    {

        [SerializeField] private DropRate m_AllDropRate = null;
        [SerializeField] private ItemDrop m_DropPrefab = null;

        #region Properties

        #endregion

        #region Methods

        protected override void GenerateDropImpl(ItemStack itemStack, Vector3 position)
        {
            ItemDrop itemDrop = Instantiate(m_DropPrefab,position,Quaternion.identity,UIManager.ItemDropsTransform);
            itemDrop.ItemStack = itemStack;
        }

        protected override DropRate[] GetDropRatesImpl(Enemy enemy)
        {
            List<DropRate> dropRates = new List<DropRate>();

            if (enemy == null) return dropRates.ToArray();

            if (m_AllDropRate != null)
            {
                dropRates.Add(m_AllDropRate);
            }

            return dropRates.ToArray();
        }

        protected override void RemoveDropRateImpl(Enemy enemy, DropTable dropTable)
        {
            throw new NotImplementedException();
        }

        protected override void SetDropRateImpl(Enemy enemy, DropTable dropTable, float rate, int count)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
