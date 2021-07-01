using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stacker : MonoBehaviour
{
    [SerializeField] private Card card = null;
    [SerializeField] private float cardSpacing = 3f;
    [SerializeField] private float positionRandom = 5f;
    [SerializeField] private float rotationRandom = 10f;
    private Stack<Card> _stack = new Stack<Card>();


    [ContextMenu("Stack")]
    private void Start()
    {
        //GameObject go = GameObject.FindGameObjectWithTag("Respawn");
        float random = positionRandom / 1000f;
        for (int i = 0; i < 100; i++)
        {
            Card _card = Instantiate(card,
                new Vector3(Random.Range(-random, random), i * (0.0075f*.22f + cardSpacing / 8000f)/2,
                    Random.Range(-random, random)),
                Quaternion.Euler(0f, Random.Range(-rotationRandom, rotationRandom), 180f));
            
            _card.transform.SetParent(transform,false);

            _card.Color = (EnkaColor) (i % 4);
            // _card.Number = i % 9;

            if (i == 99) _card.tag = "Top";
            _stack.Push(_card);
        }
    }

    public Card Take()
    {
        Card c = _stack.Pop();
        _stack.Peek().tag = "Top";
        return c;
    }
}
