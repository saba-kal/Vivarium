using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Security.Policy;
using System.Linq;

namespace Assets.Scripts.UI
{
    public class RewardsUIController : MonoBehaviour
    {
        public GameObject RewardScreen;
        public Button NextLevel;
        public Button Option1;
        public Button Option2;
        public Button Option3;
        public Image Option1Icon;
        public Image Option2Icon;
        public Image Option3Icon;
        private int selectedReward = 0;

        private void Start()
        {
            Option1.onClick.AddListener(() =>
            {
                selectedReward = 0;
            });
            Option2.onClick.AddListener(() =>
            {
                selectedReward = 1;
            });
            Option3.onClick.AddListener(() =>
            {
                selectedReward = 2;
            });
        }
        public void ShowRewardsScreen(System.Action callback, List<Item> possibleRewards)
        {
            RewardScreen.SetActive(true);
            
            var numberOfAttributes = 3;
            var rewards = possibleRewards.OrderBy(x => Random.Range(0, 100)).Take(numberOfAttributes);
            Option1Icon.sprite = possibleRewards[0].Icon;
            Option2Icon.sprite = possibleRewards[1].Icon;
            Option3Icon.sprite = possibleRewards[2].Icon;

            NextLevel.onClick.AddListener(() =>
            {
                InventoryManager.PlacePlayerItem(possibleRewards[selectedReward]);
                LogPlayerInventory();
                callback();
                RewardScreen.SetActive(false);
            });
        }
        private void LogPlayerInventory()
        {
            var logMessage = "Player inventory: ";
            foreach (var inventoryItem in InventoryManager.GetPlayerInventory().Values)
            {
                logMessage += $"[Name={inventoryItem.Item.Name},Count={inventoryItem.Count}] ";
            }

            Debug.Log(logMessage);
        }
    }
}