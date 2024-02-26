using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Flatformer.GameData
{

    #region Class: ShopData
    [System.Serializable]
    public class ShopData
    {
        public List<int> purchaseAsCharacters = new List<int>();

        public bool GetPurchaseAsCharacter(int index)
            => purchaseAsCharacters.Contains(index);

        public void AddPurchaseCharacter(int index)
            => purchaseAsCharacters.Add(index);

    }
    #endregion



    #region Class: Player Data
    [System.Serializable]
    public class PlayerData
    {
        public int coins;
        public int level;

        public int selectCharacterIndex;

        // Coin
        public int GetCoin
        {
            get { return coins; }
        }
        public void AddCoins(int coin)
            => coins += coin;

        public void SpendCoins(int coin)
            => coins -= coin;

        public bool CanSpenCoin(int coin)
            => this.coins >= coin;

        // Level
        public int GetLevel()
            => level;

        public void AddLevel(int value)
            => level += value;

        public void SetSelectChracter(int index)
            => selectCharacterIndex = index;

        public int GetSelectCharacter()
            => selectCharacterIndex;

    }
    #endregion

    public static class GameDataManager
    {
        public static PlayerData playerData;
        private static ShopData _shopData;


        private const string PLAYER_DATA = "player_data";
        private const string SHOP_DATA = "shop_data";

        static GameDataManager()
        {
            playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(PLAYER_DATA));

            _shopData = JsonUtility.FromJson<ShopData>(PlayerPrefs.GetString(SHOP_DATA));
            if (playerData == null)
            {
                int currentCoin = 50;
                int currentLevel = 0;
                int currentSkin = 0;
                playerData = new PlayerData
                {
                    coins = currentCoin,
                    level = currentLevel,
                    selectCharacterIndex = currentSkin,
                };
                SavePlayerData();
            }
            if (_shopData == null)
            {
                int curreentIndex = 0;
                _shopData = new ShopData
                {
                    purchaseAsCharacters = new List<int> { curreentIndex }
                };
                SaveShopData();
            }
        }
        private static void SavePlayerData()
        {
            var date = JsonUtility.ToJson(playerData);
            PlayerPrefs.SetString(PLAYER_DATA, date);
        }

        private static void SaveShopData()
        {
            var data = JsonUtility.ToJson(_shopData);
            PlayerPrefs.SetString(SHOP_DATA, data);
        }

        public static void ResetProgress()
        {
            playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(PLAYER_DATA));

            _shopData = JsonUtility.FromJson<ShopData>(PlayerPrefs.GetString(SHOP_DATA));

            int currentCoin = 50;
            int currentLevel = 0;
            int currentSkin = 0;
            playerData = new PlayerData
            {
                coins = currentCoin,
                level = currentLevel,
                selectCharacterIndex = currentSkin,

            };
            SavePlayerData();

            int curreentIndex = 0;
            _shopData = new ShopData
            {
                purchaseAsCharacters = new List<int> { curreentIndex }
            };
            SaveShopData();
            Debug.Log("ResetProgress");

        }


        public static void SetCharacterIndex(int index)
        {
            playerData.SetSelectChracter(index);
            SavePlayerData();
        }

        public static int GetCharacterIndex() => playerData.GetSelectCharacter();


        // Handle Player Data
        public static void AddCoin(int coin)
        {
            playerData.AddCoins(coin);
            SavePlayerData();
        }

        public static void SpendCoin(int coin)
        {
            playerData.SpendCoins(coin);
            SavePlayerData();
        }

        public static int GetCoin()
            => playerData.GetCoin;
        public static bool CanSpenCoin(int value)
            => playerData.CanSpenCoin(value);



        public static int GetLevel()
            => playerData.GetLevel();

        public static void AddLevel(int value)
        {
            playerData.AddLevel(value);
            SavePlayerData();
        }

        //Handle Shop

        public static bool GetPurchaseAsCharacter(int index) => _shopData.GetPurchaseAsCharacter(index);

        public static void AddPurchaseCharacter(int index)
        {
            _shopData.AddPurchaseCharacter(index);
            SaveShopData();
        }

    }
}
