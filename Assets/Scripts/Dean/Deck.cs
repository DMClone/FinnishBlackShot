using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<GameObject> cards = new List<GameObject>();

    private void Start()
    {
        ShuffleDeck();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShuffleDeck();
        }
    }

    public void ShuffleDeck()
    {
        cards = cards.OrderBy(x => Random.value).ToList();
    }
}
