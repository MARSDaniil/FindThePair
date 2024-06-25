using Cysharp.Threading.Tasks;
using HandyTools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class CombatGameMode : AManager<CombatGameMode> {

    #region Fields
    private GameStage m_currentGameStage = GameStage.FirstCard;

    private Card m_firstCard;
    private Card m_secondCard;

    [SerializeField] private Card m_cardPrefab;
    private List<Card> m_cards = new List<Card>();

    private int m_loop;
    public int loop {
        get { return m_loop; }
    }

    private List<Player> m_players = new List<Player>();
    public List<Player> players { 
        get { return m_players; } 
    }
    private int m_currentPlayerIndex = 0;

    private int m_summCollectedPair = 0;
    private int countOfPair = 20;
    public bool canRotateCard {
        get {
            if(m_currentGameStage == GameStage.FirstCard || m_currentGameStage == GameStage.SecondCard) {
                return true;
            }
            return false;
        }
    }
    public GameStage currentGameStage {
        get {
            return m_currentGameStage;
        }
        set {
            m_currentGameStage = value;
        }
    }

    public UICombat UI;

    #endregion

    #region Unity Event Functions

    private void Start() {
        BeginGame();
    }

    #endregion

    #region Helpers

    private async UniTask BeginGame() {
        currentGameStage = GameStage.FirstCard;
        await InitSprites();
        RandomizeCards();

        InitPlayers();
        RandomizePlayer();

        UI.SetPlayerState(m_players[m_currentPlayerIndex]);
        UI.SetLoop(m_loop);

    }

    private async UniTask InitSprites() {
        
        int capacity = _GHeart.Constants.AdressPath.KidPackCapacity;
        string iconPath = _GHeart.Constants.AdressPath.KidsPathPack;

        int backIndex = 0;


        Sprite backSprite = null;


        if (GameInstance.Exist) {
            countOfPair = GameInstance.countOfPair;
            iconPath = GameInstance.I.GetPackPath();
            capacity = GameInstance.I.GetPackCapacity();

            backIndex = GameInstance.I.GetBackCapacity();
        }

        backSprite = await Addressables.LoadAssetAsync<Sprite>($"{_GHeart.Constants.AdressPath.BackgroundPack}/{backIndex}.jpg");
        List<int> indexList = new List<int>();

        for (int i = 0; i < countOfPair; i++) {

            bool hasIndex = false;
            int randomIndex = 0;

            while (!hasIndex) {
                randomIndex = UnityEngine.Random.Range(0, capacity);
                if (!indexList.Contains(randomIndex)) {
                    indexList.Add(randomIndex);
                    hasIndex = true;
                }
            }

            Card.CardState cardState = new Card.CardState {
                id = i,
                sprite = await Addressables.LoadAssetAsync<Sprite>($"{iconPath}/{randomIndex}.jpg")
            };
            for (int j = 0; j < 2; j++) {
                Card card = Instantiate(m_cardPrefab, UI.cardsParent);

                card.Init(cardState);
                card.SetBack(backSprite);
            }
        }
    }

    private void RandomizeCards() {
        for (int i = 0; i < 5000; i++) {
            int index = UnityEngine.Random.Range(0, UI.cardsParent.transform.childCount);
            
            UI.cardsParent.GetChild(index).SetAsLastSibling();
        }
    }

    private void InitPlayers() {
        int playerCount = 5;

        if (GameInstance.Exist) {
            playerCount = GameInstance.countOfPlayer;
        }
        for(int i = 0; i < playerCount; i++) {
            Player player = new Player() {
                index = i,
                collectedCount = 0,
            };
            m_players.Add(player);
        }
    }

    private void RandomizePlayer() {
        for (int i = 0; i < 500; i++) {
            int index1 = UnityEngine.Random.Range(0, m_players.Count);
            int index2 = UnityEngine.Random.Range(0, m_players.Count);
            Player player1 = m_players[index1];
            Player player2 = m_players[index2];

            m_players[index2] = player1;
            m_players[index1] = player2;
        }
    }

    private async UniTask ShowCards() {
        if (m_firstCard && m_secondCard) {
            if (m_firstCard.cardState.id == m_secondCard.cardState.id) {
                //TODO current player increase score

                m_firstCard.FindedCard();
                m_secondCard.FindedCard();
                await UniTask.Delay(TimeSpan.FromSeconds(1f), false);

                Player newPlayer = m_players[m_currentPlayerIndex];
                newPlayer.collectedCount++;
                m_summCollectedPair++;
                m_players[m_currentPlayerIndex] = newPlayer;

                Debug.Log("U find the pair");
            } else {
                float time = 3f;

                if (Config.Exist && GameInstance.Exist) {
                    time = Config.I.gameParametrs.GetLevelDiffucultsTime(GameInstance.levelDiffucul);
                }
               
                await UniTask.Delay(TimeSpan.FromSeconds(time), false);

                m_firstCard.CloseCard();
                m_secondCard.CloseCard();

                m_currentPlayerIndex++;

                if(m_currentPlayerIndex >= m_players.Count) {
                    m_loop++;
                    UI.SetLoop(m_loop);
                    m_currentPlayerIndex = 0;
                }

                Debug.Log("U didt find the pair");
            }

            UI.SetPlayerState(m_players[m_currentPlayerIndex]);

            m_firstCard = null;
            m_secondCard = null;

            if (m_summCollectedPair < countOfPair) {
                currentGameStage = GameStage.FirstCard;
            } else {
                currentGameStage = GameStage.GameEnd;

                UI.GameOver();
            }

        } else {
            Debug.LogError("Cant find cards");
        }
    }

    #endregion


    #region Public

    public void SetCard(Card a_card) {
        switch(currentGameStage) {
            case GameStage.FirstCard:
                if (m_firstCard == null) {
                    m_firstCard = a_card;
                    currentGameStage = GameStage.SecondCard;
                } else {
                    Debug.LogError("First card isnt null");
                }
                break;
            case GameStage.SecondCard:
                if (m_secondCard == null) {
                    m_secondCard = a_card;
                    currentGameStage = GameStage.ShowCards;
                    ShowCards();
                } else {
                    Debug.LogError("Second card isnt null");
                }
                break;
            default:
                Debug.LogError("You try set card in wrong stage");
                break;
        }
    }

  
    #endregion

}

public enum GameStage {
    FirstCard,
    SecondCard,
    ShowCards,
    GameEnd
}

public struct Player {
    public int index;
    public int collectedCount;
}
