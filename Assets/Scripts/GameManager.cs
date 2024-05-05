using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameManager() {}
    
    public void Start() 
    {
        float start_time = Time.time;

        List<Card> set_list = GenerateSetList();

        Player p1 = new Player();
        Player p2 = new Player();

        creer_deck_joueur(p1, set_list);
        creer_deck_joueur(p2, set_list);

        float win_rate = 0;

        Game game = new Game();

        for (int i = 0; i < 500; i ++)
        {
            Player winner = game.play_game(p1, p2);
            if (winner == p1)
            {
                win_rate += 1;
            }
        }

        for (int i = 0; i < 500; i ++)
        {
            Player winner = game.play_game(p2, p1);
            if (winner == p1)
            {
                win_rate += 1;
            }
        }

        monte_carlo(p1, p2, win_rate, set_list, game);
    }

    public List<Card> GenerateSetList()
    {
        List<Card> newSetList = new List<Card>();
        for (int atk = 0; atk <= 12; atk++)
        {
            for (int def = 1; def <= 12; def++)
            {
                Card card = new Card(atk, def, newSetList.Count -1);
                if (card.cost <= 6)
                {
                    newSetList.Add(card);
                }
            }
        }

        return newSetList;
    }

    private bool DeckDoesntContainCardTwice(Card element, List<Card> deck)
    {
        int count = 0;

        foreach (Card value in deck)
        {
            if (value == element)
            {
                count++;
                if (count > 2) return false;
            }
        }

        return true;
    }

    public void creer_deck_joueur(Player p, List<Card> set_list)
    {
        while (p.deck.Count < 30)
        {
            Card new_card = set_list[Random.Range(0, set_list.Count)];
            if (DeckDoesntContainCardTwice(new_card, p.deck))
            {
                p.deck.Add(new_card);
            }
        }
    }

    public void monte_carlo(Player p1, Player p2, float win_rate, List<Card> set_list, Game game)
    {
        float win_rate_percent = win_rate/1000 * 100;

        Debug.Log("OLD WIN RATE : " + win_rate_percent);
        
        List<Card> new_set_list = new List<Card>();
        for (int i = 0; i < set_list.Count; i ++)
        {
            new_set_list.Add(set_list[i]);
        }

        for (int i = 0; i < 200; i ++)
        {
            Card old_card = new_set_list[Random.Range(0, new_set_list.Count)];
            p1.deck.Remove(old_card);

            Card new_card = set_list[Random.Range(0, set_list.Count)];

            while (p1.deck.Count < 30)
            {
                if (DeckDoesntContainCardTwice(new_card, p1.deck))
                {
                    p1.deck.Add(new_card);
                }

                new_card = set_list[Random.Range(0, set_list.Count)];
            }

            float current_win_rate = 0;

            for (int j = 0; j < 500; j ++)
            {
                Player winner = game.play_game(p1, p2);
                if (winner == p1)
                {
                    current_win_rate += 1;
                }
            }

            for (int j = 0; j < 500; j ++)
            {
                Player winner = game.play_game(p2, p1);
                if (winner == p1)
                {
                    current_win_rate += 1;
                }
            }

            float current_win_rate_percent = current_win_rate/1000 * 100;
            if (current_win_rate_percent > win_rate_percent)
            {
                win_rate_percent = current_win_rate_percent;
            }
            else
            {
                p1.deck.Remove(new_card);
                p1.deck.Add(old_card);

                if (current_win_rate_percent <= (current_win_rate/3) )
                {
                    new_set_list.Remove(new_card);
                }
            }
        }

        Debug.Log("NEW WIN RATE : " + win_rate_percent);
    }

    public void Update() {}

}