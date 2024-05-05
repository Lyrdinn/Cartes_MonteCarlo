import sys
import math
import random

class Joueur :
    def __init__(self) :
        self.max_mana = 0
        self.mana = 0
        self.pdv = 20
        self.deck = []
        self.pioche = []
        self.hand = []
        self.board = []

def reset_partie(joueur) :
    joueur.pdv = 20
    joueur.mana = 0
    joueur.hand = []
    joueur.board = []
    
    joueur.pioche = joueur.deck.copy()

    for i in range(0, 4) :
        card_hand = random.choice(joueur.pioche)
        joueur.hand.append(card_hand)
        joueur.pioche.remove(card_hand)

def jouer_partie(j1, j2) :
    reset_partie(j1)
    reset_partie(j2)

    while (j1.pdv > 0 and j2.pdv > 0) :
        jouer_tour(j1, j2)

        jouer_tour(j2, j1)

        if (len(j1.pioche) == 0 and len(j2.pioche) == 0) :
            return None
    
    #egalite
    if (j1.pdv < 0 and j2.pdv < 0) :
        return None
    #j2 win
    if (j1.pdv < 0) :
        return j1
    #j1 win
    if (j2.pdv < 0) :
        return j2

def jouer_tour(joueur, joueur_adv) :
    #incrementer mana
    joueur.max_mana += 1
    joueur.mana = joueur.max_mana

    #pioche une carte
    if (len(joueur.pioche) > 0) :
        pioche = random.choice(joueur.pioche)
        joueur.hand.append(pioche)
        joueur.pioche.remove(pioche)

    #poser carte
    joue_cartes(joueur)

    #faire somme des atk et enlever pdv à l'autre joueur
    somme_atk = 0
    for card in joueur.board :
        somme_atk += card.atk

    joueur_adv.pdv -= somme_atk

#retourne toutes les cartes qui ont un cout de mana M
def take_a_card_current_mana(hand, mana) :
    for card in hand :
        if (card.cost == mana) :
            return card

    return None

def joue_cartes(joueur) :
    card_mana = None

    while (joueur.mana > 0) :
        if (joueur.mana > 6) :
            card_mana = take_a_card_current_mana(joueur.hand, 6)
        else :
            card_mana = take_a_card_current_mana(joueur.hand, joueur.mana)

        if (card_mana != None) :
            joueur.board.append(card_mana)
            joueur.hand.remove(card_mana)
            joueur.mana -= card_mana.cost
        else :
            joueur.mana -= 1