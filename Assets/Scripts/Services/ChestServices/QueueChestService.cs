using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class QueueChestService : MonoBehaviour, IGameService
    {
        [SerializeField]
        private int maxNumberOfChestToEnque = 2;

        private List<ChestController> unlockingChestsQueue = new List<ChestController>();

        public void RegisterService(TypesOfServices type, IGameService gameService)
        {
            ServiceLocator.Instance.Register<QueueChestService>(type, (QueueChestService)gameService);
        }

        private void OnEnable()
        {
            RegisterService(TypesOfServices.ChestQueue, this);
        }

        public void EnqueChest(ChestController chestController)
        {
            if (unlockingChestsQueue.Count <= 0)
            {
                unlockingChestsQueue.Add(chestController);
                chestController.ChangeState(StatesOfChest.Unlocking);
            }
            else if (unlockingChestsQueue.Count < maxNumberOfChestToEnque)
            {
                unlockingChestsQueue.Add(chestController);
            }
            else
            {
                ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnQueueIsFullEventTrigger();
            }
        }

        public void DequeChest()
        {
            ChestController unlockedChest = unlockingChestsQueue[0];
            unlockedChest.ChangeState(StatesOfChest.Unlocked);
            unlockingChestsQueue.RemoveAt(0);

            UnlockNextChest();
        }

        public bool DequeChest(ChestController chestController)
        {
            ChestController unlockedChest = unlockingChestsQueue.Find(chestObject => chestObject.Equals(chestController));

            if (unlockedChest != null)
            {
                unlockedChest.ChangeState(StatesOfChest.Unlocked);
                unlockingChestsQueue.Remove(chestController);
                UnlockNextChest();
                return true;
            }

            return false;
        }

        private void UnlockNextChest()
        {
            if (unlockingChestsQueue.Count > 0)
            {
                unlockingChestsQueue[0]?.ChangeState(StatesOfChest.Unlocking);
            }
        }
    }
}