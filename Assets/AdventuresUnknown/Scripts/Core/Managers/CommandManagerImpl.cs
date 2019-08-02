using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.AdventuresUnknown.Core.Managers
{
    public class CommandManagerImpl : CommandManager
    {

        private List<ConsoleCommand> m_ConsoleCommands = new List<ConsoleCommand>();

        #region Properties

        #endregion

        #region Methods

        public static string[] GetHelpForCommand(string command)
        {
            CommandManagerImpl commandManagerImpl = Instance as CommandManagerImpl;
            if (!commandManagerImpl) return new string[0];
            string[] splits = command.Split(' ');
            if (splits.Length > 1)
            {
                foreach (ConsoleCommand consoleCommand in commandManagerImpl.m_ConsoleCommands)
                {
                    if (consoleCommand.CommandName.ToLower().Equals(splits[0].ToLower()))
                    {
                        string[] options = consoleCommand.GetOptions(splits.Length - 2, splits[splits.Length - 1]);

                        if (options == null || options.Length == 1 && options[0].Equals(splits[splits.Length - 1])) return null;

                        return options;
                    }
                }
                return null;
            }
            List<String> listOfCommands = new List<string>();
            foreach (ConsoleCommand consoleCommand in commandManagerImpl.m_ConsoleCommands)
            {
                if (consoleCommand.CommandName.StartsWith(splits[0]))
                    listOfCommands.Add(consoleCommand.CommandName);
            }
            if (listOfCommands.Count == 1 && listOfCommands[0].Equals(splits[0])) return null;
            return listOfCommands.ToArray();
        }

        protected override void AddCommandImpl(ConsoleCommand consoleCommand)
        {
            m_ConsoleCommands.Add(consoleCommand);
        }

        protected override void ExecuteCommandImpl(string command)
        {
            string[] splits = command.Split(' ');
            string[] parameters = new string[splits.Length - 1];
            for(int i = 1; i < splits.Length; i++)
            {
                parameters[i - 1] = splits[i];
            }
            bool found = false;
            foreach(ConsoleCommand consoleCommand in m_ConsoleCommands)
            {
                if (consoleCommand.CommandName.ToLower().Equals(splits[0].ToLower()))
                {
                    found = true;
                    consoleCommand.Evaluate(parameters);
                }
            }
            if (!found)
            {
                GameConsole.LogWarningFormat("No command found with name \"{0}\"", splits[0]);
            }
        }

        protected override List<ConsoleCommand> ListCommandsImpl()
        {
            return m_ConsoleCommands;
        }
        #endregion
    }
}
