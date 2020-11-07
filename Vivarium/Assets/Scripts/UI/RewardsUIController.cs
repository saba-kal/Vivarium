using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class RewardsUIController : MonoBehaviour
    {
        public GameObject RewardScreen;
        public Button NextLevel;

        public void ShowRewardsScreen(System.Action callback)
        {
            RewardScreen.SetActive(true);
            NextLevel.onClick.AddListener(() =>
            {
                callback();
                RewardScreen.SetActive(false);
            });
        }
    }
}