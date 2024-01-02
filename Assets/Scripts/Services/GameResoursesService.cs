using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ChestSystem
{
    public class GameResoursesService : MonoBehaviour, IGameService
    {

        public int Coins { get; private set; }
        public int Gems { get; private set; }

        private void OnEnable()
        {
            RegisterService(TypesOfServices.Resources, this);
        }

        private void Start()
        {
            AddCoins(1000);
            AddGems(100);
        }

        public void RegisterService(TypesOfServices type, IGameService gameService)
        {
            ServiceLocator.Instance.Register<GameResoursesService>(type, (GameResoursesService)gameService);
        }

        public bool UseGems(int value)
        {
            if (Gems >= value)
            {
                Gems -= value;
                CallGemsChanedEvent();
                return true;
            }
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnNotEnoughResoursesTrigger();
            return false;
        }

        public void AddGems(int value)
        {
            Gems += value;
            CallGemsChanedEvent();
        }

        void CallGemsChanedEvent()
        {
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events)?.OnGemsChanged(Gems);
        }

        public bool UseCoins(int value)
        {
            if (Coins >= value)
            {
                Coins -= value;
                CallCoinsChangedEvent();
                return true;
            }
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events).OnNotEnoughResoursesTrigger();
            return false;
        }

        public void AddCoins(int value)
        {
            Coins += value;
            CallCoinsChangedEvent();
        }

        void CallCoinsChangedEvent()
        {
            ServiceLocator.Instance.GetService<EventsService>(TypesOfServices.Events)?.OnCoinsChanged(Coins);
        }
    }
}