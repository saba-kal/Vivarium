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
        public List<GameObject> buttonList = new List<GameObject>();
        public Image NailMace;
        public Image BowAndGraphiteArrows;
        public Image FountainGauntlet;
        public Image ExactoCleaver;

        public void ShowRewardsScreen(System.Action callback, List<Item> possibleRewards)
        {
            RewardScreen.SetActive(true);
            
            var numberOfAttributes = 3;
            var rewards = possibleRewards.OrderBy(x => Random.Range(0, 100)).Take(numberOfAttributes);
            foreach (var reward in possibleRewards)
            {
                Option1.GetComponent<Image>().sprite = reward.Icon;
                Option2.GetComponent<Image>().sprite = reward.Icon;
                Option3.GetComponent<Image>().sprite = reward.Icon;
            }
            

            NextLevel.onClick.AddListener(() =>
            {
                callback();
                RewardScreen.SetActive(false);
            });
        }
    }
}