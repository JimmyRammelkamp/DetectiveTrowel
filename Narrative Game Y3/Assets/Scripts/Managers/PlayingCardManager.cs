using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardStatus
{
    Hand = 0,
    CloseUp = 1,
    CardSlot = 2,
    Drag = 3
}

public enum CardType
{
    None = 0,
    Character = 1,
    Motive = 2,
    Weapon = 3
}

public class PlayingCardManager : MonoBehaviour
{
    public static PlayingCardManager instance;

    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private Transform characterPosition;
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private Transform motivePosition;

    [SerializeField] private Transform fanOutCardsPosition;
    [SerializeField] private Transform closeUpCardPosition;
    [SerializeField] private Transform cardSlots;
    [SerializeField] private Transform selectedCardsOnTablePositions;

    public List<PlayCardsSObject> cardList = new();
    private List<Transform> characterList = new();
    private List<Transform> weaponList = new();
    private List<Transform> motiveList = new();

    private Transform[] slotCards = new Transform[3];

    private List<Vector3> cardPositions = new();
    private List<float> cardRotationZ = new();


    private int testIndex = 0;
    private float cardThickness;

    private bool isCardFaningDone = true;
    private bool isDragging = false;

    private CardType currentCardType;

    private IEnumerator fanOutCards;

    public CardType GetCurrentCardType() { return currentCardType; }
    public Transform GetFanOutCardsPosition() { return fanOutCardsPosition; }
    public Transform GetCloseUpCardPosition() { return closeUpCardPosition; }
    public bool IsCardFaningDone() { return isCardFaningDone; }
    public bool IsDragging() { return isDragging; }
    public void SetIsDragging(bool _bool) { isDragging = _bool; }

    void Awake()
    {
        if (instance != null) Debug.Log("Error: There are multiple instances exits at the same time (PlayingCardManager)");
        instance = this;
    }

    private void Start()
    {
        cardSlots.gameObject.SetActive(false);
        cardThickness = cardPrefab.GetComponent<MeshRenderer>().bounds.size.z;

        LoadCardsForTesting();
        for (int i = 0; i < 15; i++) AddCardTest();
    }

    private void AddCardTest()
    {
        AddCard(cardList[testIndex]);
        testIndex++;
    }

    private void LoadCardsForTesting()
    {
        Object[] Items = Resources.LoadAll("Cards", typeof(PlayCardsSObject));
        for (int i = 0; i < 12; i++)
        {
            foreach (PlayCardsSObject item in Items)
            {
                Instantiate(item);
                cardList.Add(item);
            }
        }
    }

    public List<Transform> ReturnCurrentCardList()
    {
        switch (currentCardType)
        {
            case CardType.Character:
                return characterList;

            case CardType.Motive:
                return motiveList;

            case CardType.Weapon:
                return weaponList;

            default:
                return null;
        }
    }

    private void PutCardsAaway(List<Transform> _cardList)
    {
        switch (currentCardType)
        {
            case CardType.Character:
                SetCardsBack(ref _cardList, characterPosition);
                break;
            case CardType.Weapon:
                SetCardsBack(ref _cardList, weaponPosition);
                break;
            case CardType.Motive:
                SetCardsBack(ref _cardList, motivePosition);
                break;
        }
    }

    public void CloseCardMenu()
    {
        cardSlots.gameObject.SetActive(false);
        PutCardsAaway(ReturnCurrentCardList());
    }

    public void InteractWithDeck(CardType _type)
    {
        cardSlots.gameObject.SetActive(true);

        if (ReturnCurrentCardList() != null) PutCardsAaway(ReturnCurrentCardList());

        currentCardType = _type;
        CheckCloseUpCards();
        RearrangeCards();
        FanOutCards(true);
    }

    public void PlaceCardBack()
    {
        RearrangeCards();
        FanOutCards(false);
    }

    private void SetCardsBack(ref List<Transform> _list, Transform _parent)
    {
        int counter = 0;
        foreach (var card in _list)
        {
            if (card.GetComponent<PlayingCard>().GetCardStatus() == CardStatus.CardSlot) continue;

            card.SetParent(_parent);

            card.transform.localPosition = Vector3.zero;
            card.transform.rotation = Quaternion.Euler(new Vector3(90, 180, _parent.eulerAngles.y + Random.Range(-10.0f, 10.0f)));

            if (counter > 0) card.transform.localPosition = _list[counter - 1].localPosition + new Vector3(0, cardThickness, 0);

            counter++;
        }
    }

    public void CheckCloseUpCards()
    {
        foreach (var card in ReturnCurrentCardList())
        {
            if (card.GetComponent<PlayingCard>().GetCardStatus() == CardStatus.CloseUp) card.GetComponent<PlayingCard>().SetCardStatus(CardStatus.Hand);
        }
    }

    private void RearrangeCards()
    {
        foreach (var card in ReturnCurrentCardList())
        {
            if (card.GetComponent<PlayingCard>().GetCardStatus() != CardStatus.Hand) continue;
            card.SetParent(null);
            card.SetParent(fanOutCardsPosition);
        }
    }

    private void FanOutCards(bool _deckInteraction)
    {
        cardPositions.Clear();
        cardRotationZ.Clear();

        float radius = 3;

        int cardsNumber = fanOutCardsPosition.childCount;
        float fromToAngle = 30;
        if (cardsNumber < 4) fromToAngle = cardsNumber * 10;

        float angleStep = fromToAngle / cardsNumber;
        float currentAngle = -(fromToAngle / 2) + 90 + (angleStep / 2);
        float startZ = 0;

        for (int i = 0; i < cardsNumber; i++)
        {
            float x = radius * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float y = radius * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

            Vector3 position = new Vector3(x, y, startZ);

            if (_deckInteraction && i == 0) InstantiateDeckBeforeFanOut(position, currentAngle - 90);

            cardPositions.Add(position);
            cardRotationZ.Add(currentAngle - 90);

            currentAngle += angleStep;
            startZ += cardThickness * 2.0f;
        }

        LerpCards();
    }

    private void InstantiateDeckBeforeFanOut(Vector3 _position, float _rotationZ)
    {
        float startZ = 0;
        for (int i = 0; i < fanOutCardsPosition.childCount; i++)
        {
            fanOutCardsPosition.GetChild(i).localPosition = _position + new Vector3(0, 0, startZ);
            fanOutCardsPosition.GetChild(i).localRotation = Quaternion.Euler(new Vector3(0, 0, _rotationZ));
            startZ += cardThickness * 1.5f;
        }
    }

    public void LerpCards()
    {
        if (fanOutCards != null) StopCoroutine(fanOutCards);
        fanOutCards = LerpCardsIE();
        StartCoroutine(fanOutCards);
    }

    IEnumerator LerpCardsIE()
    {
        isCardFaningDone = false;

        List<Vector3> startPositions = new();
        List<float> startRotation = new();

        for (int i = 0; i < fanOutCardsPosition.childCount; i++)
        {
            startPositions.Add(fanOutCardsPosition.GetChild(i).localPosition);
            startRotation.Add(fanOutCardsPosition.GetChild(i).localRotation.eulerAngles.z);
        }

        float lerp = 0;

        while (lerp < 1)
        {
            yield return new WaitForSeconds(0.01f);

            lerp += 0.035f;

            for (int i = 0; i < fanOutCardsPosition.childCount; i++)
            {
                fanOutCardsPosition.GetChild(i).localPosition = Vector3.Lerp(startPositions[i], cardPositions[i], lerp);
                fanOutCardsPosition.GetChild(i).localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.LerpAngle(startRotation[i], cardRotationZ[i], lerp)));
            }
        }
        isCardFaningDone = true;
    }

    public void AddCard(PlayCardsSObject _playCards)
    {
        CardType type = _playCards.Type;
        GameObject go = Instantiate(cardPrefab);

        go.GetComponent<PlayingCard>().SetCardData(_playCards);

        switch (type)
        {
            case CardType.Character:
                SetCardPosition(ref go, ref characterList, characterPosition);
                break;
            case CardType.Weapon:
                SetCardPosition(ref go, ref weaponList, weaponPosition);
                break;
            case CardType.Motive:
                SetCardPosition(ref go, ref motiveList, motivePosition);
                break;
        }
    }

    private void SetCardPosition(ref GameObject _card, ref List<Transform> _list, Transform _parent)
    {
        _list.Add(_card.transform);
        _card.transform.SetParent(_parent);
        _card.transform.localPosition = Vector3.zero;
        _card.transform.rotation = Quaternion.Euler(new Vector3(90, 180, _parent.eulerAngles.y + Random.Range(-10.0f, 10.0f)));

        if (_list.Count > 1) _card.transform.localPosition = _list[_list.Count - 2].localPosition + new Vector3(0, cardThickness, 0);
    }

    public void ToggleSlotOutline(bool _toggle)
    {
        for (int i = 0; i < cardSlots.childCount; i++)
        {
            if (cardSlots.GetChild(i).GetComponent<CardSlot>().GetSlotType() == currentCardType)
            {
                GameManager.instance.ToggleOutline(cardSlots.GetChild(i).gameObject, _toggle);
            }
        }
    }

    public void RemoveCardFromSlot(int _index)
    {
        _index--;

        if (slotCards[_index] == null) return;

        slotCards[_index] = null;
        Destroy(selectedCardsOnTablePositions.GetChild(_index).GetChild(0).gameObject);
    }

    public void AddCardToSlot(Transform _card)
    {
        int typeIndex = (int)_card.GetComponent<PlayingCard>().GetCardData().Type - 1;
        slotCards[typeIndex] = _card;

        if (selectedCardsOnTablePositions.GetChild(typeIndex).childCount > 0) Destroy(selectedCardsOnTablePositions.GetChild(typeIndex).GetChild(0).gameObject);

        GameObject go = Instantiate(cardPrefab, selectedCardsOnTablePositions.GetChild(typeIndex));
        go.GetComponent<PlayingCard>().SetCardData(_card.GetComponent<PlayingCard>().GetCardData());
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.Euler(Vector3.zero);

        if (go.transform.TryGetComponent(out BoxCollider collider))
        {
            Destroy(collider);
        }
    }
}
