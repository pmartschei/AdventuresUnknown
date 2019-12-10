using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Objects.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventuresUnknown.Scripts.Core.Logic.Play
{
    public class ActiveGemData
    {
        public ActiveGem ActiveGem { get; private set; }
        public Entity Entity { get; private set; }
        public string[] ModTypes { get; private set; }

        public ActiveGemData(ActiveGem activeGem, Entity entity, string[] modTypes)
        {
            ActiveGem = activeGem;
            Entity = entity;
            ModTypes = modTypes;
        }
    }
}
