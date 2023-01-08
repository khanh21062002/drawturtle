import random
from words import words
import string

def get_valid_word(words):
    word = random.choice(words) # randomly choose something from the list
    while '-' in word or ' ' in word:
        word = random.choice(words)
    
    return word.upper()

def hangman():
    
    word = get_valid_word(words)
    word_letters = set(word) # letters in the word
    alphabet = set(string.ascii_uppercase)
    used_letters = set() # what the user has guessed
    
    lives = 10

    # getting user input 
    while len(word_letters) > 0 and lives > 0:
        # letters used
        # ' '.join (['a','b','cd']) --> 'a b cd'
        print('you have',lives, 'life you have used these letters: ', ' '.join(used_letters))

        # what current word is (ie W - R D)
        word_list = [letter if letter in used_letters else '-' for letter in word]
        print('Current word: ', ' '.join(word_list))

        user_letter = input('guess a letter: ').upper()
        if user_letter in alphabet - used_letters:
            used_letters.add(user_letter)
            if user_letter in word_letters:
                word_letters.remove(user_letter)
            else:
                lives = lives - 1
                print('letter is not in word.')
        elif user_letter in used_letters:
            print('You have already used that character. please try again. ')

        else:
            print('Invalid character. please try again.')
    # get here when len(word_letters)==0 or when lives ==0 
    if lives ==0 :
        print('you died, sorry. the word was ', word)
    else:
        print('you guessed the word', word, '!!!')
hangman()