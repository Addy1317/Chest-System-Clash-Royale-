using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class ChestCollectedState : ChestBaseState
    {
        private ChestController chestController;

        public ChestCollectedState(ChestController chestController)
        {
            this.chestController = chestController;
        }

        public override void OnEnterState()
        {
            ReturnTheChestToPool();
        }

        public override void OnExitState()
        {

        }

        public override void Tick()
        {

        }

        private void ReturnTheChestToPool()
        {
            throw new NotImplementedException();
        }
    }
}