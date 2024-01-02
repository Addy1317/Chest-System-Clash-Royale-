using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ChestSystem
{
    public class PopupPanelUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject popupPanel;
        [SerializeField]
        private TextMeshProUGUI popupText, unlockImmidiatelyText;
        [SerializeField]
        private Button unlockButton, unlockImmidiateButton, closePopupButton;

        private int requiredGemsToUnlock = 0;

        private ChestController selectedChestController;

        private void Start()
        {
            closePopupButton.onClick.AddListener(ClosePopupPanelPressed);
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnAllSlotsAreFilledEvent += OnChestSlotsFilled;
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnChestSelectedEvent += OnChestSelectedEventTriggered;
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnNotEnoughResoursesEvent += OnNotEnoughResoursesTriggered;
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnQueueIsFullEvent += OnQueueFilledTriggered;

            unlockButton.onClick.AddListener(onUnlockButtonPressed);
            unlockImmidiateButton.onClick.AddListener(onUnlockImmididiateButtonPressed);
        }

        public void OnChestSlotsFilled()
        {
            popupText.text = "All the Slots are Occupied";
            DeactivateBottomPopup();
            popupPanel.SetActive(true);
        }

        public void OnNotEnoughResoursesTriggered()
        {
            popupText.text = "Not Enough Resourses";
            DeactivateBottomPopup();
            popupPanel.SetActive(true);
        }

        public void OnQueueFilledTriggered()
        {
            popupText.text = "Queue is Full";
            DeactivateBottomPopup();
            popupPanel.SetActive(true);
        }

        private void DeactivateBottomPopup()
        {
            unlockButton.gameObject.SetActive(false);
            unlockImmidiateButton.gameObject.SetActive(false);
        }

        public void ClosePopupPanelPressed()
        {
            popupPanel.SetActive(false);
        }

        private void OnDestroy()
        {
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnAllSlotsAreFilledEvent -= OnChestSlotsFilled;
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnChestSelectedEvent -= OnChestSelectedEventTriggered;
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnNotEnoughResoursesEvent -= OnNotEnoughResoursesTriggered;
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnQueueIsFullEvent -= OnQueueFilledTriggered;
        }

        public void OnChestSelectedEventTriggered(int remainingTimeToUnlockInMinutes, ChestController chestController)
        {
            popupText.text = "Start Unlocking the Chest";
            unlockButton.gameObject.SetActive(true);
            unlockImmidiateButton.gameObject.SetActive(true);

            requiredGemsToUnlock = (remainingTimeToUnlockInMinutes + 1) * 3;
            unlockImmidiatelyText.text = "Unlock " + requiredGemsToUnlock + " Gems";
            popupPanel.SetActive(true);
            selectedChestController = chestController;
        }

        public void onUnlockButtonPressed()
        {
            popupPanel.SetActive(false);
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnChestUnlockPressedEventTrigger(selectedChestController);
        }

        public void onUnlockImmididiateButtonPressed()
        {
            popupPanel.SetActive(false);
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnUnlockImmidiatelyPressedTrigger(
                requiredGemsToUnlock, selectedChestController);
        }
    }
}