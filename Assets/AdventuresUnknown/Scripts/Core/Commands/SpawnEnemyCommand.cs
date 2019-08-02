using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Enemies;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdventuresUnknown.Core.Commands
{
    public class SpawnEnemyCommand : ConsoleCommand
    {
        #region Properties

        #endregion

        #region Methods

        #endregion
        public SpawnEnemyCommand(string commandName) : base(commandName)
        {
            m_Usage = "Usage: " + commandName + " <enemyname>";
        }
        public override void Evaluate(params string[] parameters)
        {
            if (parameters.Length == 0)
            {
                GameConsole.Log(Usage);
                return;
            }
            Enemy enemy = ObjectsManager.FindObjectByIdentifier<Enemy>(parameters[0]);
            if (!enemy)
            {
                GameConsole.LogWarningFormat("No Enemy found with identifier {0}", parameters[0]);
                return;
            }
            LevelManager.SpawnEnemy(enemy,Vector3.zero);
        }

        public override string[] GetOptions(int param, string startingText)
        {
            List<string> listOfOptions = new List<string>();
            if (param == 0)
            {
                Enemy[] enemies = ObjectsManager.GetAllObjects<Enemy>();

                foreach(Enemy enemy in enemies)
                {
                    if (enemy.Identifier.StartsWith(startingText))
                    {
                        listOfOptions.Add(enemy.Identifier);
                    }
                }
            }

            return listOfOptions.ToArray();
        }
    }
}
