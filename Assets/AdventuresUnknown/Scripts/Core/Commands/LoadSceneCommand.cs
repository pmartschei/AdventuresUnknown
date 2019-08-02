using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventuresUnknown.Core.Commands
{
    public class LoadSceneCommand : ConsoleCommand
    {


        #region Properties

        #endregion

        #region Methods
        public LoadSceneCommand(string commandName) : base(commandName)
        {
            m_Usage = "Usage: " + commandName + " <sceneName>";
        }
        #endregion
        public override void Evaluate(params string[] parameters)
        {
            if (parameters.Length == 0)
            {
                GameConsole.Log(Usage);
                return;
            }
            string sceneName = parameters[0];
            if (!SceneManager.IsValidScene(sceneName))
            {
                GameConsole.LogWarningFormat("No valid scene found with name {0}", sceneName);
            }
            SceneManager.LoadScene(sceneName);
        }

        public override string[] GetOptions(int param, string startingText)
        {
            return null;
        }
    }
}
