
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ChestSystem
{
    public class ChestUnlockingState : ChestBaseState
    {
        private ChestController chestController;
        private TextMeshProUGUI chestTimerText;
        private int timeIntervalToUpdateText;
        private float timeToUnlock;
        private float currentTime;

        private Button chestButton;

        public ChestUnlockingState(int timeIntervalToUpdateText, TextMeshProUGUI chestTimerText,
            ChestController chestController, Button chestButton)
        {
            this.chestTimerText = chestTimerText;
            this.timeIntervalToUpdateText = timeIntervalToUpdateText;
            this.chestController = chestController;
            this.timeToUnlock = chestController.GetUnlockTime();
            currentTime = timeIntervalToUpdateText;
            this.chestButton = chestButton;
        }

        public override void OnEnterState()
        {
            UpdateTimerText();
            chestButton.onClick.AddListener(OnChestButtonPressed);
        }

        public override void OnExitState()
        {
            chestButton.onClick.RemoveListener(OnChestButtonPressed);
        }

        public override void Tick()
        {
            if (timeToUnlock <= 0)
            {
                return;
            }

            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                timeToUnlock -= timeIntervalToUpdateText;
                currentTime = timeIntervalToUpdateText;
                if (timeToUnlock <= 0)
                {
                    timeToUnlock = 0;
                    OnChestUnlocked();
                }
                UpdateTimerText();
            }
        }

        public void UpdateTimerText()
        {
            int minutes = (int)(timeToUnlock / 60);
            int seconds = (int)(timeToUnlock % 60);

            chestTimerText.text = minutes + " : " + seconds;
        }

        public void OnChestUnlocked()
        {
            chestController.OnChestUnlocked();
        }

        public void OnChestButtonPressed()
        {
            chestController.OnChestSelected(timeToUnlock);
        }
    }
}