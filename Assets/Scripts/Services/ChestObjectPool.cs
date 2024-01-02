using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class ChestObjectPool : IGameService
    {
        class ChestPoolData
        {
            public ChestController chestController;
            public bool isActive;
        }

        private List<ChestPoolData> chestPool;

        public ChestObjectPool()
        {
            chestPool = new List<ChestPoolData>();
        }

        public void RegisterService(TypesOfServices type, IGameService gameService)
        {
            ServiceLocator.Instance.Register<ChestObjectPool>(type, (ChestObjectPool)gameService);
        }

        public ChestController GetChest()
        {
            ChestPoolData chestPoolObject = chestPool.Find(chestObject => !chestObject.isActive);

            if (chestPoolObject != null)
            {
                chestPoolObject.isActive = true;
                return chestPoolObject.chestController;
            }

            return null;
        }

        public void ReturnChestObject(ChestController chestController)
        {
            ChestPoolData chestPoolObject = chestPool.Find(chestObject => chestObject.chestController.Equals(chestController));

            if (chestPoolObject != null)
            {
                chestPoolObject.isActive = false;
            }
            else
            {
                chestPoolObject = new ChestPoolData();
                chestPoolObject.isActive = false;
                chestPoolObject.chestController = chestController;
                chestPool.Add(chestPoolObject);
            }
        }
    }
}