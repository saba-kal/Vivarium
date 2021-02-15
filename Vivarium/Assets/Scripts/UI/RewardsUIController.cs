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

        private int _selectedReward = 0;
        private System.Action _nextLevelCallback;
        private List<Item> _rewards = new List<Item>();
        private List<Tooltip> _tooltips = new List<Tooltip>();
        private List<Character> _characters;

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
            NextLevel.onClick.AddListener(() =>
            {
                if (_nextLevelCallback != null)
                {
                    if (CharacterReward.rewardLevel)
                    {
                        SaveSelectedCharacter();
                    }
                    else
                    {
                        PlaceSelectedReward(_rewards);
                        LogPlayerInventory();
                    }
                    RewardScreen.SetActive(false);
                    _selectedReward = 0;
                    _nextLevelCallback();
                }
            });
        }

        public void ShowRewardsScreen(System.Action callback, LootTable possibleRewards)
        {
            if (CharacterReward.rewardLevel)
            {
                CharacterRewardsScreen(callback);
            }
            else
            {
                ItemRewardsScreen(callback, possibleRewards);
            }
        }
        private void CharacterRewardsScreen(System.Action callback)
        {
            UpdateButtons();
            RewardScreen.SetActive(true);
            _nextLevelCallback = callback;
            UpdateCharacters();

            Option1Icon.sprite = _characters[0].Portrait;
            Option2Icon.sprite = _characters[1].Portrait;
            Option3Icon.sprite = _characters[2].Portrait;


            SetTooltipData();
        }

        private void UpdateCharacters()
        {
            _characters = new List<Character>();
            var characterControllers = new List<CharacterController>();
            foreach (var characterGameObject in CharacterReward.characterGameObjects)
            {
                characterControllers.Add(characterGameObject.GetComponent<CharacterController>());
            }

            foreach (var characterController in characterControllers)
            {
                _characters.Add(characterController.Character);
            }
        }

        private void SaveSelectedCharacter()
        {
            if (_selectedReward < CharacterReward.characterGameObjects.Count && _selectedReward >= 0)
            {
                CharacterReward.selectedCharacter = CharacterReward.characterGameObjects[_selectedReward];
            }
            else
            {
                UnityEngine.Debug.LogError("Unable to save the game object of the selected character");
            }
        }

        private void ItemRewardsScreen(System.Action callback, LootTable possibleRewards)
        {
            UpdateButtons();
            RewardScreen.SetActive(true);
            _nextLevelCallback = callback;

            _rewards = possibleRewards.Pick(3);
            Option1Icon.sprite = _rewards[0].Icon;
            Option2Icon.sprite = _rewards[1].Icon;
            Option3Icon.sprite = _rewards[2].Icon;


            SetTooltipData();
        }

        private void PlaceSelectedReward(List<Item> possibleRewards)
        {
            Item reward = GetSelectedReward(possibleRewards);
            if (reward == null)
            {
                return;
            }

            InventoryManager.PlacePlayerItem(reward);
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
            Option1.GetComponent<UnityEngine.UI.Outline>().enabled = false;
            Option2.GetComponent<UnityEngine.UI.Outline>().enabled = false;
            Option3.GetComponent<UnityEngine.UI.Outline>().enabled = false;

            switch (_selectedReward)
            {
                case 0:
                    Option1.GetComponent<UnityEngine.UI.Outline>().enabled = true;
                    break;
                case 1:
                    Option2.GetComponent<UnityEngine.UI.Outline>().enabled = true;
                    break;
                case 2:
                    Option3.GetComponent<UnityEngine.UI.Outline>().enabled = true;
                    break;
            }
        }

        private void SetTooltipData()
        {
            if(CharacterReward.rewardLevel)
            {
                SetCharacterTooltipData();
            }
            else
            {
                SetItemTooltipData();
            }
        }

        private void SetCharacterTooltipData()
        {
            if (_characters.Count < 3)
            {
                return;
            }

            ClearTooltips();

            var options = new List<Button> { Option1, Option2, Option3 };
            for (int i = 0; i < options.Count; i++)
            {
                var tooltip = options[i].GetComponent<Tooltip>();
                if (tooltip != null)
                {
                    tooltip.SetTooltipData(_characters[i]);
                }
                _tooltips.Add(tooltip);
            }
        }
        private void SetItemTooltipData()
        {
            if (_rewards.Count < 3)
            {
                return;
            }

            ClearTooltips();

            var options = new List<Button> { Option1, Option2, Option3 };
            for (int i = 0; i < options.Count; i++)
            {
                var tooltip = options[i].GetComponent<Tooltip>();
                if (tooltip != null)
                {
                    tooltip.SetTooltipData(_rewards[i]);
                }
                _tooltips.Add(tooltip);
            }
        }

        public void DoubleClicked()
        {
            if (CharacterReward.rewardLevel)
            {
                SaveSelectedCharacter();
            }
            else
            {
                PlaceSelectedReward(_rewards);
                LogPlayerInventory();
            }
            RewardScreen.SetActive(false);
            _selectedReward = 0;
            _nextLevelCallback();
            ClearTooltips();
        }

        private void ClearTooltips()
        {
            foreach (var tooltip in _tooltips)
            {
                tooltip.HideTooltip();
            }
            _tooltips = new List<Tooltip>();
        }
    }
}