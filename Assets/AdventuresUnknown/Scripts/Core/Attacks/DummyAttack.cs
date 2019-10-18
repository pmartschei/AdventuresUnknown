using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Entities.Weapons;
using AdventuresUnknownSDK.Core.Logic.Attacks;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdventuresUnknown.Scripts.Core.Attacks
{
    public class DummyAttack : GenericAttack
    {
        #region Properties

        #endregion

        #region Methods
        public override IEnumerator Activate(ActivationParameters activationParameters)
        {
            PreActivate(activationParameters);

            yield return null;
        }
        #endregion
    }
}
