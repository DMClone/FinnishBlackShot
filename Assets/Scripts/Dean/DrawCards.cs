using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public List<GameObject> hand = new List<GameObject>();
    public int count;
    public bool stand;
    public TextMeshProUGUI playerText;
    public GameObject AceValueChoice;

    [SerializeField] private Deck deck;
    [SerializeField] private Transform handAnchor;
    [SerializeField] private float cardSpacing = 1.5f;
    [SerializeField] private TextMeshProUGUI currentValueText;

    public Card currentCard;

    private void Start()
    {
        AceValueChoice.SetActive(false);
    }

    private void Update()
    {
        currentValueText.text = "Current Value: " + count;
    }

    public void DrawCard()
    {
        if (deck.cards.Count == 0) return;

        GameObject topCard = deck.cards[0];
        GameObject newCard = Instantiate(topCard, handAnchor);

        Vector3 offset = new Vector3(cardSpacing * hand.Count, 0, 0);
        newCard.transform.localPosition = offset;

        if (handAnchor != null)
        {
            newCard.transform.localRotation = Quaternion.identity;
        }

        currentCard = newCard.GetComponent<Card>();
        if (currentCard.value == 0)
        {
            AceValueChoice.SetActive(true);
            hand.Add(newCard);
            deck.cards.RemoveAt(0);
            return;
        }
        count += currentCard.value;
        hand.Add(newCard);
        deck.cards.RemoveAt(0);
    }

    public void NewStart()
    {
        foreach (var card in hand)
        {
            if (card != null)
                Destroy(card);
        }

        hand.Clear();
        count = 0;
        stand = false;
    }
}
