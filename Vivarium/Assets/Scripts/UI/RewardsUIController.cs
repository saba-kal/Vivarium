﻿using UnityEngine;
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

        private int _selectedReward = 0;

        private void Start()
        {
            Option1.onClick.AddListener(() =>
            {
                _selectedReward = 0;
            });
            Option2.onClick.AddListener(() =>
            {
                _selectedReward = 1;
            });
            Option3.onClick.AddListener(() =>
            {
                _selectedReward = 2;
            });
        }

        public void ShowRewardsScreen(System.Action callback, List<Item> possibleRewards)
        {
            RewardScreen.SetActive(true);

            if (possibleRewards.Count >= 3)
            {
                var numberOfRewards = 3;
                var rewards = possibleRewards.OrderBy(x => Random.Range(0, 100)).Take(numberOfRewards);
                Option1Icon.sprite = possibleRewards[0].Icon;
                Option2Icon.sprite = possibleRewards[1].Icon;
                Option3Icon.sprite = possibleRewards[2].Icon;

                NextLevel.onClick.AddListener(() =>
                {
                    PlaceSelectedReward(rewards.ToList());
                    LogPlayerInventory();
                    callback();
                    RewardScreen.SetActive(false);
                });
            }
        }

        private void PlaceSelectedReward(List<Item> possibleRewards)
        {
            Item reward = GetSelectedReward(possibleRewards);
            if (reward == null)
            {
                return;
            }

            var randomCharacterId = GetRandomCharacterIdWithEmptyInventory();
            if (string.IsNullOrEmpty(randomCharacterId))
            {
                return;
            }

            InventoryManager.PlaceCharacterItem(randomCharacterId, reward);
        }

        public Item GetSelectedReward(List<Item> possibleRewards)
        {
            if (_selectedReward < possibleRewards.Count)
            {
                return possibleRewards[_selectedReward];
            }
            else
            {
                return null;
            }
        }

        public string GetRandomCharacterIdWithEmptyInventory()
        {
            var playerCharacters = TurnSystemManager.Instance?.PlayerController?.PlayerCharacters;
            if (playerCharacters == null)
            {
                return null;
            }

            var characterIdsWithEmptyInventory = new List<string>();
            foreach (var characterController in playerCharacters)
            {
                if (InventoryManager.GetCharacterItemCount(characterController.Id) < Constants.MAX_CHARACTER_ITEMS)
                {
                    characterIdsWithEmptyInventory.Add(characterController.Id);
                }
            }

            if (characterIdsWithEmptyInventory.Count == 0)
            {
                return null;
            }

            return characterIdsWithEmptyInventory[Random.Range(0, characterIdsWithEmptyInventory.Count)];
        }

        private void LogPlayerInventory()
        {
            var logMessage = "Player inventory: ";
            foreach (var inventoryItems in InventoryManager.GetPlayerInventory().Values)
            {
                foreach (var inventoryItem in inventoryItems)
                {
                    logMessage += $"[Name={inventoryItem.Item.Name},Count={inventoryItem.Count}] ";
                }
            }

            Debug.Log(logMessage);
        }
    }
}