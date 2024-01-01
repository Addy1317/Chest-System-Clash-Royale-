using UnityEngine;

namespace ChestSystem
{
    public class ChestModel
    {
        private ChestController chestController;
        public ChestScriptableObject chestScriptable;
        public float timeToUnlockInSeconds;

        public ChestModel(ChestController chestController, ChestScriptableObject chestScriptableObject)
        {
            this.chestController = chestController;
            ResetChestData(chestScriptableObject);
        }

        public void ResetChestData(ChestScriptableObject chestScriptableObject)
        {
            this.chestScriptable = chestScriptableObject;
            timeToUnlockInSeconds = chestScriptable.timeInMinutes * 60;
        }
    }
}
