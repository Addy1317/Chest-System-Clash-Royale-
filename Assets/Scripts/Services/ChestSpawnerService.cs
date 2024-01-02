using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class ChestSpawnerService : MonoBehaviour, IGameService
    {
        [SerializeField]
        private ChestView chestView;
        [SerializeField]
        private ChestScritableObjectList ChestsList_Scriptable;
        [SerializeField]
        private Transform parentObjectOfChests;
        [SerializeField]
        private int maxNumberOfChest = 3;
        [SerializeField]
        private int costPerChest = 50;

        private ChestObjectPool chestObjectPool;

        public void RegisterService(TypesOfServices type, IGameService gameService)
        {
            ServiceLocator.Instance.Register<ChestSpawnerService>(type, (ChestSpawnerService)gameService);
        }

        void Start()
        {
            RegisterService(TypesOfServices.ChestSpawner, this);
            chestObjectPool = new ChestObjectPool();

            for (int i = 0; i < maxNumberOfChest; i++)
            {
                SpawnChestController();
            }
        }

        public void SpawnChestController()
        {
            ChestController chestController = new ChestController(
                    ChestsList_Scriptable.chestScriptableList[Random.Range(0, ChestsList_Scriptable.chestScriptableList.Count)],
                    chestView, parentObjectOfChests);

            ReturnChestController(chestController);
        }

        public void ReturnChestController(ChestController chestController)
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
                    chestController.Enable(ChestsList_Scriptable.chestScriptableList[
                        Random.Range(0, ChestsList_Scriptable.chestScriptableList.Count)]);
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
