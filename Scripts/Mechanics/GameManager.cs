using Flatformer.GameData;
using Platformer;
using Platformer.Model;
using Platformer.Observer;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using YG;


namespace Platformer.Mechanics
{
    public class GameManager : MonoBehaviour
    {

        #region Singleton Class: GameManager
        public static GameManager instance;

        private void OnEnable()
        {
            if (instance == null)
            {
                instance = this;
            }
            this.RegisterListener(EventID.Start, (param) => StartGame());
            this.RegisterListener(EventID.Replay, (param) => ReplayGame());
        }
        #endregion


        public PlatformerModel model;

        public int currentLevels;

        private GameObject objLevel;

        private const string IS_WIN = "IsWin";
        private const string IS_DEATH = "IsDeath";

        public List<GameObject> listEffect = new List<GameObject>();
        public int Coin { get; set; }


        private void OnDisable()
        {
            if (EventDispatcher.HasInstance())
            {
                EventDispatcher.Instance.RemoveListener(EventID.Start, (param) => StartGame());
                EventDispatcher.Instance.RemoveListener(EventID.Replay, (param) => ReplayGame());
            }
        }

        // Create clone gameObjetc level

        public void StartGame()
        {
            if (objLevel != null)
                Destroy(objLevel.gameObject);
            int randomBG = Random.Range(0, 3);
            model.backGrounds[randomBG].gameObject.SetActive(true);
            for (int i = 0; i < model.backGrounds.Count; i++)
            {
                if (i != randomBG)
                {
                    model.backGrounds[i].gameObject.SetActive(false);
                }
            }
            if (GameDataManager.GetLevel() >= model.levels.Count)
            {
                currentLevels = Random.Range(10, 30);
            }
            else
            {
                currentLevels = GameDataManager.GetLevel();
            }
            objLevel = Instantiate(model.levels[currentLevels], transform);
        }


        public void ReplayGame()
        {
            if (objLevel != null)
            {
                Destroy(objLevel.gameObject);
            }
            foreach (GameObject effect in listEffect)
            {
                Destroy(effect.gameObject);
            }
            objLevel = Instantiate(model.levels[currentLevels], transform) as GameObject;
        }


        public void NextLevel()
        {
            if (currentLevels > model.levels.Count)
                this.PostEvent(EventID.EndGame);
            if (objLevel != null)
                Destroy(objLevel.gameObject);
            foreach (GameObject effect in listEffect)
            {
                Destroy(effect.gameObject);
            }

            int randomBG = Random.Range(0, 3);
            model.backGrounds[randomBG].gameObject.SetActive(true);
            for (int i = 0; i < model.backGrounds.Count; i++)
            {
                if (i != randomBG)
                {
                    model.backGrounds[i].gameObject.SetActive(false);
                }
            }
            if (GameDataManager.GetLevel() >= model.levels.Count)
            {
                currentLevels = Random.Range(10, 30);
            }
            else
            {
                ++currentLevels;
            }

            objLevel = Instantiate(model.levels[currentLevels], transform) as GameObject;
        }


        public void PlayerDeath(PlayerController playerRef)
        {
            var player = playerRef;
            if (player != null)
            {
                player.isControlEnable = false;
                player.transform.rotation = Quaternion.Euler(0, 180f, 0);
                SoundManager.instance.PlayAudioSound(player.deathAudio);
                player._myAnimator.SetBool(IS_DEATH, true);
            }
        }

        public void PlayerWin(PlayerController playerRef)
        {
            var player = playerRef;
            if (player != null)
            {
                player.isControlEnable = false;
                player.transform.rotation = Quaternion.Euler(0, 180f, 0);
                player._myAnimator.SetBool(IS_WIN, true);

                if (GameDataManager.GetLevel() <= currentLevels)
                {
                    YandexGame.NewLeaderboardScores("MaxLevel", currentLevels + 1);
                    Debug.Log("CurrentLevelBIG: " + (currentLevels+1));
                }
            }
        }

    }

}


