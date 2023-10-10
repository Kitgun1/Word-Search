using KiYandexSDK;
using UnityEngine;

namespace _Project.Scripts
{
    public class AdvertShow : MonoBehaviour
    {
        public void ShowAdvert() => AdvertSDK.InterstitialAd();
        public void ShowRewardAd() => AdvertSDK.RewardAd();
    }
}