using System.Collections.Generic;
using UnityEngine;

public class Game {
    public Game() {}
    
    public void reset_game(Player player)
    {
        player.pdv = 20;
        player.mana = 0;
        player.hand = new List<Card>();
        player.board = new List<Card>();

        player.pioche = new List<Card>();

        for (int i = 0; i < player.deck.Count; i ++)
        {
            player.pioche.Add(player.deck[i]);
        }

        for (int i = 0; i < player.max_hand_size; i ++)
        {
            Card c = player.pioche[Random.Range(0, player.pioche.Count)];
            player.hand.Add(c);
            player.pioche.Remove(c);
        }
    }

    public Player play_game(Player p1, Player p2)
    {
        reset_game(p1);
        reset_game(p2);

        while (p1.pdv > 0 && p2.pdv > 0)
        {
            play_turn(p1, p2);
            play_turn(p2, p1);

            if (p1.pioche.Count < 0 && p2.pioche.Count < 0)
            {
                return null;
            }
        }

        if (p1.pdv < 0 && p2.pdv < 0) return null;
        if (p1.pdv < 0) return p2;
        else return p1;
    }

    public void play_turn(Player p, Player p_adv) 
    {
        p.max_mana += 1;
        p.mana = p.max_mana;

        while (p.hand.Count < p.max_hand_size && p.pioche.Count > 0)
        {
            Card c = p.pioche[Random.Range(0, p.pioche.Count)];
            p.hand.Add(c);
            p.pioche.Remove(c);
        }

        while (PlayHighestCostCard(p)) ;

        int somme_atk = 0;

        for (int i = 0; i < p.board.Count; i ++)
        {
            somme_atk += p.board[i].atk;
        }

        p_adv.pdv -= somme_atk;
    }

    public Card take_hand_current_mana(List<Card> hand, int mana)
    {
        for (int i = 0; i < hand.Count; i ++)
        {
            if (hand[i].cost == mana) return hand[i];
        }

        return null;
    }

    public bool PlayHighestCostCard(Player p)
    {
        Card highestCostCard = null;
        int highestCost = 0;

        foreach (var card in p.hand)
        {
            if (card.cost > highestCost && card.cost <= p.mana)
            {
                highestCostCard = card;
            }
        }

        if (highestCostCard != null)
        {
            p.mana -= highestCostCard.cost;
            p.board.Add(highestCostCard);
            p.hand.Remove(highestCostCard);
            return true;
        }

        // if we couldn't place any card because their cost is too high;
        return false;
    }
}