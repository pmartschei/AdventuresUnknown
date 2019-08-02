using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventuresUnknown.Core.Commands
{
    public class GenerateNewLevelsCommand : ConsoleCommand
    {
        #region Properties

        #endregion
        #region Methods
        public GenerateNewLevelsCommand(string commandName) : base(commandName)
        {
            Usage = "Usage: "+ commandName;
        }
        public override void Evaluate(params string[] parameters)
        {
            JourneyData jd = ObjectsManager.FindObjectByIdentifier<JourneyData>("core.datas.journey");
            if (!jd) return;
            jd.GenerateNextLevels();
        }

        public override string[] GetOptions(int param, string startingText)
        {
            return null;
        }
        #endregion
    }
}
