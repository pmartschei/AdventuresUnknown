using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventuresUnknown.Core.Commands
{
    public class CreateItemCommand : ConsoleCommand
    {
        #region Properties

        #endregion

        #region Methods

        #endregion
        public CreateItemCommand(string commandName) : base(commandName)
        {
            m_Usage = "Usage: " + commandName + " <itemname> <amount> <inventoryName>";
        }
        public override void Evaluate(params string[] parameters)
        {
            if (parameters.Length == 0)
            {
                GameConsole.Log(Usage);
                return;
            }
            Item item = ObjectsManager.FindObjectByIdentifier<Item>(parameters[0]);
            if (!item)
            {
                GameConsole.LogWarningFormat("No Item found with identifier {0}", parameters[0]);
                return;
            }
            int amount = item.DefaultAmount;
            if (parameters.Length >= 2)
            {
                if (!int.TryParse(parameters[1], out amount))
                {
                    GameConsole.LogWarningFormat("Amount could not get parsed, using default amount = {0}", item.DefaultAmount);
                    amount = item.DefaultAmount;
                }
            }
            string inventoryIdentifier = "core.inventories.inventory";
            if (parameters.Length >= 3)
            {
                inventoryIdentifier = parameters[2];
            }
            Inventory inventory = ObjectsManager.FindObjectByIdentifier<Inventory>(inventoryIdentifier);
            if (!inventory)
            {
                GameConsole.LogWarningFormat("No Inventory found with identifier {0}", parameters[2]);
                return;
            }
            if (!inventory.AddItemStack(item.CreateItem(amount)))
            {
                GameConsole.LogWarning("Inventory is full");
            }
        }

        public override string[] GetOptions(int param, string startingText)
        {
            List<string> listOfOptions = new List<string>();
            if (param == 0)
            {
                Item[] items = ObjectsManager.GetAllObjects<Item>();

                foreach(Item item in items)
                {
                    if (item.Identifier.StartsWith(startingText))
                    {
                        listOfOptions.Add(item.Identifier);
                    }
                }
            }else if (param == 2)
            {
                Inventory[] inventories = ObjectsManager.GetAllObjects<Inventory>();

                foreach (Inventory inventory in inventories)
                {
                    if (inventory.Identifier.StartsWith(startingText))
                    {
                        listOfOptions.Add(inventory.Identifier);
                    }
                }
            }

            return listOfOptions.ToArray();
        }
    }
}
