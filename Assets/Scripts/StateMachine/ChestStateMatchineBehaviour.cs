using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace ChestSystem
{
    public class ChestStateMatchineBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Button chestButton;
        [SerializeField]
        private TextMeshProUGUI chestStateText;

        private ChestBaseState currentChestState = null;

        public void ChangeChestState(StatesOfChest stateOfChest, ChestController chestController)
        {
            if (currentChestState != null)
            {
                currentChestState.OnExitState();
                currentChestState = null;
            }

            switch (stateOfChest)
            {
                case StatesOfChest.Locked:
                    currentChestState = new ChestLockedState(chestStateText, chestButton, chestController);
                    break;
                case StatesOfChest.Unlocking:
                    currentChestState = new ChestUnlockingState(1, chestStateText, chestController, chestButton);
                    break;
                case StatesOfChest.Unlocked:
                    currentChestState = new ChestUnlockedState(chestButton, chestController, chestStateText);
                    break;
                case StatesOfChest.Collected:
                    currentChestState = new ChestCollectedState(chestController);
                    break;
            }
            currentChestState?.OnEnterState();
        }

        private void Update()
        {
            currentChestState?.Tick();
        }
    }
}