using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem
{
    public class ChestView : MonoBehaviour
    {
        private ChestController chestController;

        [SerializeField]
        private Image chestImage;

        [SerializeField]
        private GameObject chestVisual;

        [SerializeField]
        private private ChestStateMatchineBehaviour chestStateMachine;

        public void SetChestController(ChestController chestController)
        {
            this.chestController = chestController;
        }

        public void EnableChest(Sprite sprite)
        {
            this.chestImage.sprite = sprite;
            chestVisual.SetActive(true);
            changeChestState(StateOfChest.Locked);
        }

        public void Disable()
        {
            chestVisual.SetActive(false);
        }

        public void ChangeChestState(StateOfChest newState)
        {
            chestStateMachine.ChangeChestState(newState, this.chestController);
        }
    }
}