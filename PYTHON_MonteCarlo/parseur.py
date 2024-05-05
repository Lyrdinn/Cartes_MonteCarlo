from cartes import *

def parse(path) :
    fd = open(path, "r")
    lines = fd.readlines()

    card_set_list = []

    for line in lines :
        line_array = line.rstrip().split()
        
        atk = int(line_array[3])
        defense = int(line_array[5])
        cost = (atk + defense)/2

        new_card = Card(cost, atk, defense)
        card_set_list.append(new_card)
    
    return card_set_list
