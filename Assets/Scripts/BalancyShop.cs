using Balancy;
using UnityEngine;
using Balancy.Interfaces;
using Balancy.Models.SmartObjects;
using Balancy.Models.LiveOps.Store;
using Balancy.Platforms.Nutaku;
using System;
using Nutaku.Unity;

public class BalancyShop: MonoBehaviour
{
    private string apiGameId = "d7dfb896-7cef-11ef-9498-066676c39f77";
    private string publicKey = "NjNlZTJlNzk2MmVhNTI0ZTIxNWI5YW";

    [SerializeField] private BalancyShopItem balancyShopItemPrefab;
    [SerializeField] private Transform content;

    void Awake()
    {
        //SdkPlugin.Initialize();
    }

    protected void Start()
    {
        InitBalancy();
    }

    private void InitBalancy()
    {
        Main.Init(new AppConfig
        {
            ApiGameId = apiGameId,
            PublicKey = publicKey,

            Environment = Constants.Environment.Development,
            AutoLogin = true,
            //OfflineMode = false,            

            Platform = Constants.Platform.NutakuAndroid,

            NutakuConfig = new NutakuConfig() { ConsumerSecret = "F3urZ3Tlu6rd]0]qROFQ[bWJbKLzt9WY", ConsumerKey = "p1Kn9yIfHaZbuUvt" },
            //PreInit = PreInitType.None,
            OnInitProgress = progress => {
                Debug.Log($"***=> STATUS {progress.Status}");
                switch (progress.Status)
                {
                    case BalancyInitStatus.PreInitFromResourcesOrCache:
                        //CMS, loaded from resource or cache is ready, invoked only if PreInit >= PreInitType.LoadStaticData
                        break;
                    case BalancyInitStatus.PreInitLocalProfile:
                        //Local profile is loaded, invoked only if PreInit >= PreInitType.LoadStaticDataAndLocalProfile
                        break;
                    case BalancyInitStatus.DictionariesReady:
                        //CMS is updated and ready
                        break;
                    case BalancyInitStatus.Finished:
                        //All systems are ready
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            },
            UpdateType = UpdateType.FullUpdate,
            UpdatePeriod = 5,
            OnContentUpdateCallback = updateResponse =>
            {
                Debug.Log("Content Updated " + updateResponse.AffectedDictionaries.Length);
            },
            OnReadyCallback = responseData => {
                Debug.Log("Balancy Initialized: " + responseData.Success);
                if (!responseData.Success)
                    Controller.PrintAllErrors();
                else
                {                    
                    Debug.LogWarning("Balancy Initialized: Success");
                    Debug.LogWarning("USER " + LiveOps.Profile.GeneralInfo.ProfileId);

                    //Payments.InvalidateCache();

                    NutakuNetwork.NutakuInstance.Login(result =>
                    {
                        if (!result.Success)
                            Debug.LogError("Nutaku auth error: " + result.Error.Code + " => " + result.Error.Message);
                        else
                            InitShop();
                    });

                }
            }
        });
    }

    private void InitShop()
    {
        var allOffers = LiveOps.GameOffers.GetActiveOffers();
        ExternalEvents.RegisterLiveOpsListener(new LiveOpsStoreEventsExample());
        //Debug.LogError(LiveOps.Store.DefaultStore.ActivePages.Count);
        foreach (var page in LiveOps.Store.DefaultStore.ActivePages)
        {
            if (page.Name.ToString() == "Gems")
            {
                foreach (var slot in page.ActiveSlots)
                {
                    BalancyShopItem balancyShopItem = Instantiate(balancyShopItemPrefab, content);
                    balancyShopItem.Init(slot.GetStoreItem());
                }
            }
        }
        //DataEditor.
        foreach (var gameOffer in DataManager.SmartObjects.GameOffers)
        {

        }

        foreach (var storeItem in DataManager.SmartObjects.StoreItems)
        {
            if (storeItem.IntUnnyId == 685)
            {
                //priceText.SetText(storeItem.Price.Product.Price.ToString());
            }
        }
    }
}

public class LiveOpsStoreEventsExample : IStoreEvents
{
    public void OnStoreResourcesMultiplierChanged(float multiplier)
    {
        Debug.Log("=> OnStoreResourcesMultiplierChanged: " + multiplier);
    }

    public void OnStoreUpdated(GameStoreBase storeConfig)
    {
        Debug.Log("=> OnStoreUpdated: " + storeConfig.UnnyId);
    }

    public void OnStorePageUpdated(GameStoreBase storeConfig, Page page)
    {
        Debug.Log("=> OnStorePageUpdated: " + storeConfig.UnnyId + " page = " + page.Name.Value);
    }
}
