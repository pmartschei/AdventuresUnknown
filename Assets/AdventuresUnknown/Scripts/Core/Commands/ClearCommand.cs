using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventuresUnknown.Core.Commands
{
    public class ClearCommand : ConsoleCommand
    {
        #region Properties

        #endregion

        #region Methods

        #endregion
        public ClearCommand(string commandName) : base(commandName)
        {
            m_Usage = "Usage: " + commandName;
        }
        public override void Evaluate(params string[] parameters)
        {
            LogManager.ClearLog();
        }

        public override string[] GetOptions(int param, string startingText)
        {
            return null;
        }
    }
}
