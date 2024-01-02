using UnityEngine;
using System;

namespace ChestSystem
{
    public class EventsService : MonoBehaviour, IGameService
    {
        public event Action<int> OnGemsChangedEvent;
        public event Action<int> OnCoinsChangedEvent;
        public event Action OnAllSlotsAreFilledEvent;
        public event Action OnNotEnoughResoursesEvent;
        public event Action OnQueueIsFullEvent;
        public event Action<int, ChestController> OnChestSelectedEvent;
        public event Action<ChestController> unlockChestPressedEvent;
        public event Action<int, ChestController> unlockImmidiatelyPressedEvent;

        private void OnEnable()
        {
            RegisterService(TypesOfServices.Events, this);
        }

        public void RegisterService(TypesOfServices type, IGameService gameService)
        {
            if (ServiceLocator.Instance == null)
            {
                Debug.Log("Instance is Null");
            }
            ServiceLocator.Instance.Register<EventsService>(type, (EventsService)gameService);
        }

        public void OnGemsChanged(int currentNumberOfGems)
        {
            OnGemsChangedEvent?.Invoke(currentNumberOfGems);
        }

        public void OnCoinsChanged(int currentNumberOfCoins)
        {
            OnCoinsChangedEvent?.Invoke(currentNumberOfCoins);
        }

        public void OnAllChestSlotaAreFullEventTrigger()
        {
            OnAllSlotsAreFilledEvent?.Invoke();
        }

        public void OnChestSelectedEventTrigger(int remainingTimeToUnlockInMinutes, ChestController chestController)
        {
            OnChestSelectedEvent?.Invoke(remainingTimeToUnlockInMinutes, chestController);
        }

        public void OnChestUnlockPressedEventTrigger(ChestController chestController)
        {
            unlockChestPressedEvent?.Invoke(chestController);
        }

        public void OnUnlockImmidiatelyPressedTrigger(int numberOfGemsToUse, ChestController chestController)
        {
            unlockImmidiatelyPressedEvent?.Invoke(numberOfGemsToUse, chestController);
        }

        public void OnNotEnoughResoursesTrigger()
        {
            OnNotEnoughResoursesEvent?.Invoke();
        }

        public void OnQueueIsFullEventTrigger()
        {
            OnQueueIsFullEvent?.Invoke();
        }
    }
}