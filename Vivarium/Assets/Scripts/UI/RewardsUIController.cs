using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

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
        public TextMeshProUGUI ItemDescription;
        public TextMeshProUGUI ItemName;
        public TextMeshProUGUI ItemStats;

        private int _selectedReward = 0;
        private System.Action _nextLevelCallback;
        private List<Item> _rewards = new List<Item>();

        private void Start()
        {
            Option1.onClick.AddListener(() =>
            {
                _selectedReward = 0;
                UpdateItemDescription();
            });
            Option2.onClick.AddListener(() =>
            {
                _selectedReward = 1;
                UpdateItemDescription();
            });
            Option3.onClick.AddListener(() =>
            {
                _selectedReward = 2;
                UpdateItemDescription();
            });
            NextLevel.onClick.AddListener(() =>
            {
                if (_nextLevelCallback != null)
                {
                    PlaceSelectedReward(_rewards);
                    LogPlayerInventory();
                    RewardScreen.SetActive(false);
                    _selectedReward = 0;
                    UpdateItemDescription();
                    _nextLevelCallback();
                }
            });
        }

        public void ShowRewardsScreen(System.Action callback, List<Item> possibleRewards)
        {
            UpdateButtons();
            RewardScreen.SetActive(true);
            _nextLevelCallback = callback;

            if (possibleRewards.Count >= 3)
            {
                var numberOfRewards = 3;
                _rewards = possibleRewards.OrderBy(x => Random.Range(0, 100)).Take(numberOfRewards).ToList();
                Option1Icon.sprite = _rewards[0].Icon;
                Option2Icon.sprite = _rewards[1].Icon;
                Option3Icon.sprite = _rewards[2].Icon;
            }

            UpdateItemDescription();
        }

        private void PlaceSelectedReward(List<Item> possibleRewards)
        {
            Item reward = GetSelectedReward(possibleRewards);
            if (reward == null)
            {
                return;
            }

            var randomCharacter = GetRandomCharacterWithEmptyInventory(reward);
            if (string.IsNullOrEmpty(randomCharacter?.Id))
            {
                return;
            }

            InventoryManager.PlaceCharacterItem(randomCharacter.Id, reward);
            //TODO: figure out a better system for shields.
            if (reward.Type == ItemType.Shield)
            {
                randomCharacter.Equip(reward);
            }
        }

        public Item GetSelectedReward(List<Item> possibleRewards)
        {
            if (_selectedReward < possibleRewards.Count && _selectedReward >= 0)
            {
                return possibleRewards[_selectedReward];
            }
            else
            {
                return null;
            }
        }

        public CharacterController GetRandomCharacterWithEmptyInventory(Item reward)
        {
            var playerCharacters = TurnSystemManager.Instance?.PlayerController?.PlayerCharacters;
            if (playerCharacters == null)
            {
                Debug.LogError("Could not find any characters to give reward to.");
                return null;
            }

            var charactersWithEmptyInventory = new List<CharacterController>();
            foreach (var characterController in playerCharacters)
            {
                if (InventoryManager.GetCharacterItemCount(characterController.Id) < Constants.MAX_CHARACTER_ITEMS)
                {
                    charactersWithEmptyInventory.Add(characterController);
                }
            }

            if (charactersWithEmptyInventory.Count == 0)
            {
                Debug.LogWarning("Could not find any characters with an empty inventory to give reward to.");
                return null;
            }

            var eligibleCharacters = new List<CharacterController>();
            foreach (var characterController in charactersWithEmptyInventory)
            {
                if (characterController.Character?.Weapon?.Id != reward.Id &&
                    !(reward.Type == ItemType.Shield && characterController.Character?.Shield != null))
                {
                    eligibleCharacters.Add(characterController);
                }
            }

            if (eligibleCharacters.Count == 0)
            {
                Debug.LogWarning("All characters already have this item as a weapon or shield. " +
                    "Therefore, a random character with an empty inventory was chosen.");
                eligibleCharacters = charactersWithEmptyInventory;
            }

            return eligibleCharacters[Random.Range(0, eligibleCharacters.Count)];
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

        private void UpdateButtons()
        {
            Option1.GetComponent<Outline>().enabled = false;
            Option2.GetComponent<Outline>().enabled = false;
            Option3.GetComponent<Outline>().enabled = false;

            switch (_selectedReward)
            {
                case 0:
                    Option1.GetComponent<Outline>().enabled = true;
                    break;
                case 1:
                    Option2.GetComponent<Outline>().enabled = true;
                    break;
                case 2:
                    Option3.GetComponent<Outline>().enabled = true;
                    break;
            }
        }

        private void UpdateItemDescription()
        {
            if (ItemDescription != null && _selectedReward < _rewards.Count && _selectedReward >= 0)
            {
                ItemDescription.text = _rewards[_selectedReward].Description;
                ItemName.text = _rewards[_selectedReward].Name;
                if (_rewards[_selectedReward].Type == ItemType.Shield)
                {
                    ItemStats.text = "Shield: " + ((Shield)_rewards[_selectedReward]).Health;
                }
                else if (_rewards[_selectedReward].Type == ItemType.Weapon)
                {
                    var weapon = ((Weapon)_rewards[_selectedReward]);
                    ItemStats.text = "Actions: \n";
                    foreach (var Action in weapon.Actions)
                    {
                        ItemStats.text += Action.Name + " " + Action.BaseDamage + " damage \n";
                    }
                }
                else
                {
                    ItemStats.text = "Health Restored: " + ((Consumable)_rewards[_selectedReward]).value;
                }
            }
            else
            {
                ItemDescription.text = "";
                ItemStats.text = "";
                ItemName.text = "";
            }
        }
    }
}