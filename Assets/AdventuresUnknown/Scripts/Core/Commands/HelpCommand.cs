using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventuresUnknown.Core.Commands
{
    public class HelpCommand : ConsoleCommand
    {
        #region Properties

        #endregion

        #region Methods

        #endregion
        public HelpCommand(string commandName) : base(commandName)
        {
            m_Usage = "Usage: "+commandName + " <command>";
        }
        public override void Evaluate(params string[] parameters)
        {
            if (parameters.Length == 0)
            {
                StringBuilder sb = new StringBuilder("List of available commands: ");
                List<ConsoleCommand> consoleCommands = CommandManager.ListCommands();

                for (int i = 0; i < consoleCommands.Count; i++)
                {
                    sb.Append(consoleCommands[i].CommandName);
                    if (i < consoleCommands.Count - 1)
                    {
                        sb.Append(", ");
                    }
                }

                GameConsole.Log(sb.ToString());
            }
            else
            {

                List<ConsoleCommand> consoleCommands = CommandManager.ListCommands();

                for (int i = 0; i < consoleCommands.Count; i++)
                {
                    if (consoleCommands[i].CommandName.ToLower().Equals(parameters[0].ToLower()))
                    {
                        GameConsole.Log(consoleCommands[i].Usage);
                        break;
                    }
                }

            }
        }

        public override string[] GetOptions(int param, string startingText)
        {
            if (param > 0) return null;
            List<ConsoleCommand> consoleCommands = CommandManager.ListCommands();
            List<string> listOfCommands = new List<string>();

            foreach (ConsoleCommand consoleCommand in consoleCommands)
            {
                if (consoleCommand.CommandName.StartsWith(startingText))
                    listOfCommands.Add(consoleCommand.CommandName);
            }
            return listOfCommands.ToArray();
        }
    }
}
