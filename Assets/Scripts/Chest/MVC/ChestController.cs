using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class ChestController
    {
        private ChestModel chestModel;
        private ChestView chestView;
        private ChestScriptableObject chestScriptableObject;

        private ChestController (ChestScriptableObject chestScriptableObject, ChestView chestView, Transform parent)
        {
            this.chestModel = new ChestModel(this,chestScriptableObject);
            this.chestView = GameObject.Instantiate<ChestView>(chestView, parent);
            chestView.SetChestController(this);
            this.chestScriptableObject = chestScriptableObject;
        }
        public void Enable(ChestScriptableObject chestScriptableObject)
        {
            this.chestScriptableObject = chestScriptableObject;
            chestModel.ResetChestData(this.chestScriptableObject);
            chestView.SetChestController(this);
            chestView.EnableChest(this.chestScriptableObject.chestSprite);
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).unlockChestPressedEvent += OnUnlockChestPressed;
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).unlockImmidiatelyPressedEvent += OnUnlockImmidiatePressed;
        }

        public void Disabe()
        {
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).unlockChestPressedEvent -= OnUnlockChestPressed;
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).unlockImmidiatelyPressedEvent -= OnUnlockImmidiatePressed;
            chestView.Disable();
        }

      
    }
}