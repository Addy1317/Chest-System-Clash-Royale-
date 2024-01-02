using UnityEngine;
using TMPro;

namespace ChestSystem
{
    public class GameResourcesUi : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI coinsText, gemsText;

        private void OnEnable()
        {
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnCoinsChangedEvent += ChangeCoinsText;
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnGemsChangedEvent += ChangeGemsText;
        }

        private void OnDisable()
        {
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnCoinsChangedEvent -= ChangeCoinsText;
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnGemsChangedEvent -= ChangeGemsText;
        }

        void ChangeCoinsText(int value)
        {
            coinsText.text = value.ToString();
        }

        void ChangeGemsText(int value)
        {
            gemsText.text = value.ToString();
        }
    }
}