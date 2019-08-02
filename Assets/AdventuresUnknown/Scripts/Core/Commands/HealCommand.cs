using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventuresUnknown.Core.Commands
{
    public class HealCommand : ConsoleCommand
    {
        #region Properties

        #endregion
        #region Methods
        public HealCommand(string commandName) : base(commandName)
        {
            Usage = "Usage: "+ commandName;
        }
        public override void Evaluate(params string[] parameters)
        {
            //Stat lifeStat = PlayerManager.SpaceShip.GetStat("core.modtypes.life");
            //if (lifeStat != null)
            //{
            //    lifeStat.Current = lifeStat.Calculated;
            //}
        }

        public override string[] GetOptions(int param, string startingText)
        {
            return null;
        }
        #endregion
    }
}
