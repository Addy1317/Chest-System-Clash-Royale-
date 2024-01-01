
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ChestSystem
{
    public class ChestUnlockedState : ChestBaseState
    {
        private Button chestButton;
        private ChestController chestController;
        private TextMeshProUGUI chestStateText;

        public ChestUnlockedState(Button chestButton, ChestController chestController, TextMeshProUGUI chestStateText)
        {
            this.chestButton = chestButton;
            this.chestController = chestController;
            this.chestStateText = chestStateText;
        }
        public override void OnEnterState()
        {
            chestButton.onClick.AddListener(OnChestButtonPressed);
            chestStateText.text = "Unlocked";
        }

        public override void OnExitState()
        {
            chestButton.onClick.RemoveListener(OnChestButtonPressed);
        }

        public override void Tick()
        {
            //nothing to do here
        }

        public void OnChestButtonPressed()
        {
            chestController.OnChestCollected();
        }
    }
}