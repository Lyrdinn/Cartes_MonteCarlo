import sys
import math
import time
import random

from cartes import *
from parseur import *
from partie import *

def creer_deck_joueur(joueur, set_list) :
    while (len(joueur.deck) < 30) :
        new_card = random.choice(set_list)
        if (joueur.deck.count(new_card) < 2) :
            joueur.deck.append(new_card)

def monte_carlo(j1, j2, win_rate, card_set_list) :

    win_rate_percent = win_rate/100 * 100

    print("OLD WIN RATE : " + str(win_rate_percent))

    for i in range(0, 100) :
        old_card = random.choice(j1.deck)
        j1.deck.remove(old_card)

        new_card = random.choice(card_set_list)

        while (True) :
            if (j1.deck.count(new_card) < 2) :
                j1.deck.append(new_card)
                break
            else :
                new_card = random.choice(card_set_list)

        current_win_rate = 0
        
        # appel de notre algo
        for i in range(0, 50) :
            gagnant = jouer_partie(j1, j2)
            if (gagnant == j1) :
                current_win_rate += 1
        for i in range(0, 50) :
            gagnant = jouer_partie(j2, j1)
            if (gagnant == j1) :
                current_win_rate += 1

        current_win_rate_percent = current_win_rate/100 * 100

        if (current_win_rate_percent > win_rate_percent) :
            win_rate_percent = current_win_rate_percent
        else :
            j1.deck.remove(new_card)
            j1.deck.append(old_card)

        #print(current_win_rate_percent)

    print("NEW WIN RATE : " + str(win_rate_percent))

def main(path):
    start_time = time.time()

    #appel du parseur
    card_set_list = parse("setlist.txt")


    j1 = Joueur()
    j2 = Joueur()

    creer_deck_joueur(j1, card_set_list)
    creer_deck_joueur(j2, card_set_list)

    win_rate = 0

    # appel de notre algo
    for i in range(0, 50) :
        gagnant = jouer_partie(j1, j2)
        if (gagnant == j1) :
            win_rate += 1
    for i in range(0, 50) :
        gagnant = jouer_partie(j2, j1)
        if (gagnant == j1) :
            win_rate += 1

    monte_carlo(j1, j2, win_rate, card_set_list)

    print("--- %s seconds ---" % (time.time() - start_time))
 
if __name__ == "__main__":
    main(sys.argv[1])