namespace Platformer.Observer
{
    public enum EventID
    {
        None = 0,
        Home,
        Replay,
        LoadGame,
        GameStartUI,
        Start,
        IsPlayGame, // true chp tur pin false khong cho rut pin
        Victory,
        Loss,
        EndGame,
        btnSkiplevel,
        btnReward,
        // Shop
        OpenShop,
        SelectSkin,
        PurchaseSkin,
        // Event Test
        OnCarMove,
        OpenSpin,

         
    }

}