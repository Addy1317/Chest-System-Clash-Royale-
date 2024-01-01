namespace ChestSystem
{
    public interface IGameService
    {
        public void RegisterService(TypesOfServices type, IGameService gameService);
    }
}