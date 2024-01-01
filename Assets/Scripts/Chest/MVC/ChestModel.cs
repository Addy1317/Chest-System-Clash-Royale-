using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class ChestModel : MonoBehaviour
    {
        private ChestController ChestController;
        private ChestScriptableObject ChestScriptable;
        public float timeToUnlockInSeconds;

        public ChestModel(ChestController chestController,ChestScriptableObject chestScriptableObject)
        {
            this.ChestController = chestController;
            ResetChestData(chestScriptableObject);
        }

        public void ResetChestData(ChestScriptableObject chestScriptableObject)
        {
            this.ChestScriptable = chestScriptableObject;
            timeToUnlockInSeconds = chestScriptableObject.timeInMinutes * 60;
        }
    }
}
