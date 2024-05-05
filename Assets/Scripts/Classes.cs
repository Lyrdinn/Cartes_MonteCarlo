using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public int index;
    public int atk;
    public int defense;

    public int cost;

    public Card(int atk, int def, int index)
    {
        this.index = index;
        this.atk = atk;
        this.defense = def;
        this.cost = Mathf.CeilToInt((float)(atk+def)/2);
    }

    public override string ToString()
    {
        return$"i : {index} ATK : {atk} DEF : {defense}";
    }
}

public class Player {
    public int max_mana = 0;
    public int mana = 0;
    public int pdv = 20;
    public int max_hand_size = 4;
    public List<Card> deck;
    public List<Card> pioche;
    public List<Card> hand;
    public List<Card> board;

    public Player()
    {
        deck = new List<Card>();
        pioche = new List<Card>();
        board = new List<Card>();
        hand = new List<Card>();
    }
}