using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ChestSystem
{
    public class ChestLockedState : ChestBaseState
    {
        private TextMeshProUGUI chestStateText;
        private Button chestButton;
        private ChestController chestController;
        public ChestLockedState(TextMeshProUGUI chestStateText, Button chestButton, ChestController chestController)
        {
            this.chestStateText = chestStateText;
            this.chestButton = chestButton;
            this.chestController = chestController;
        }

        public override void OnEnterState()
        {
            chestStateText.text = "Locked";
            chestButton.onClick.AddListener(OnChestButtonPressed);
        }

        public override void OnExitState()
        {
            chestButton.onClick.RemoveListener(OnChestButtonPressed);
        }

        public override void Tick()
        {
            
        }

        public void OnChestButtonPressed()
        {
            chestController.OnChestSelected();
        }
    }
}