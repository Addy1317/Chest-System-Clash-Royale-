using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ChestSystem
{
    public class SpawnChestUI : MonoBehaviour
    {
        [SerializeField]
        private Button spawnChestButton;

        void Start()
        {
            spawnChestButton.onClick.AddListener(SpawnChestButtonPressed);
        }

        public void SpawnChestButtonPressed()
        {
            ServiceLocator.Instance.GetService<ChestSpawnerService>(TypesOfServices.ChestSpawner).GetChestController();
        }
    }
}