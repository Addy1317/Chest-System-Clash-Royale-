using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class ChestSpawnerService : MonoBehaviour, IGameService
    {
        [SerializeField] private ChestView chestView;
        [SerializeField] private ChestScritableObjectList chestScritableObjectList;
        [SerializeField] private Transform parentObjectOfChests;
        [SerializeField] private int maxNumberOfChest = 4;
        [SerializeField] private int costPerChest = 50;

        private ChestObjectPool chestObjectPool;

        public void RegisterService(TypesOfServices type, IGameService gameService)
        {
            ServiceLocator.Instance.Register<ChestSpawnerService>(type, (ChestSpawnerService)gameService);
        }

        void start()
        {
            RegisterService(TypesOfServices.ChestSpawner, this);
            chestObjectPool = new ChestObjectPool();

            for (int i = 0; i < maxNumberOfChest; i++)
            {
                spawnChestController();
            }
        }

        private void spawnChestController()
        {
            ChestController chestController = new ChestController(
                chestScritableObjectList.chestScriptableList[Random.Range(0, chestScritableObjectList.chestScriptableList.Count)],
                 chestView, parentObjectOfChests);

            ReturnChestController(chestController);
        }

        private void ReturnChestController(ChestController chestController)
        {
            chestObjectPool.ReturnChestObject(chestController);
            chestController.Disable();
        }
        public void GetChestController()
        {
            ChestController chestController = chestObjectPool.GetChest();

            if (chestController != null)
            {
                if (ServiceLocator.Instance.GetService<GameResoursesService>(TypesOfServices.Resources).UseCoins(costPerChest))
                {
                    chestController.Enable(chestScritableObjectList.chestScriptableList[
                        Random.Range(0, chestScritableObjectList.chestScriptableList.Count)]);
                }
                else
                {
                    ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnNotEnoughResoursesTrigger();
                }
            }
            else
            {
                ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnAllChestSlotaAreFullEventTrigger();
            }
        }
    }
}
