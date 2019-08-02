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

namespace AdventuresUnknown.Core.Commands
{
    public class SaveCommand : ConsoleCommand
    {
        #region Properties

        #endregion

        #region Methods

        #endregion
        public SaveCommand(string commandName) : base(commandName)
        {
            m_Usage = "Usage: " + commandName;
        }
        public override void Evaluate(params string[] parameters)
        {
            PlayerManager.Save();
        }

        public override string[] GetOptions(int param, string startingText)
        {
            return null;
        }
    }
}
