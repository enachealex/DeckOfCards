using System;

namespace AlexEnache
{

    public enum Suit { Clubs, Diamonds, Hearts, Spades};
    public enum Rank { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King};

    public class Card
    {
        public Suit Suit { get; private set; }
        public Rank Rank { get; private set; }
        public Card(Suit suit, Rank rank)
        {
            this.Suit = suit;
            this.Rank = rank;
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
    /// <summary>
    /// check size of deck
    /// shuffle deck
    /// cut deck
    /// deal cards to players
    /// create toString method
    /// </summary>
    public class Deck
    {
        private Card[] cards;
        private static readonly Random rand = new Random();

        public Deck()
        {
            int index = 0;
            Array suit = Enum.GetValues(typeof(Suit));
            Array rank = Enum.GetValues(typeof(Rank));

            int length = suit.Length * rank.Length;
            cards = new Card[length];

            foreach(Suit suits in suit)
                foreach(Rank ranks in rank)
                {
                    Card card = new Card(suits, ranks);
                    cards[index++] = card;
                }
        }

        public int Size()
        {
            return cards.Length;
        }

        public void Shuffle()
        {
            for (int i = 0; i < Size(); i++)
            {
                if (Size() == 0) break;
                int temp = rand.Next(i, Size());
                Card card2 = cards[i];
                cards[i] = cards[temp];
                cards[temp] = card2;
            }
        }

        public void Cut()
        {
            int index = 0;
            Card[] cutDeck = new Card[Size()];
            int cutting = rand.Next(1, Size());

            for (int i = cutting; i < Size(); i++)
                cutDeck[index++] = cards[i];
            for (int i = 0; i < cutting; i++)
                cutDeck[index++] = cards[i];
            cards = cutDeck;
        }

        public Card DealCard()
        {
            Card card = cards[Size() - 1];
            Array.Resize(ref cards, Size() - 1);
            return card;
        }

        public override string ToString()
        {
            string converted = "|";
            string colon = "";
            foreach (Card temp in cards)
            {
                converted += colon + temp.ToString();
                colon = "][";
            }
            converted += "|";
            Console.WriteLine();
            converted += $"{ Size() } cards remaining in deck";
            Console.WriteLine();
            return converted;
        }
    }

    /// <summary>
    /// constructor
    /// amount of cards in the hand
    /// player gets cards
    /// player gets card removed
    /// create toString Method
    /// </summary>
    public class Hand
    {
        private Card[] cards;

        public Hand()
        {
            cards = new Card[0];
        }

        public int Size()
        {
            return cards.Length;

        }

        public Card[] GetCards()
        {
            return cards;
        }

        public void AddCard(Card addedCard)
        {
            Array.Resize(ref cards, Size() + 1);
            cards[Size() - 1] = addedCard;
        }

        public Card RemoveCard(Card card)
        {//**************************************************
            Card[] addedCards = new Card[cards.Length - 1];
            bool removableCard = false;
            int i = 0;
            foreach(Card c in cards)
            {
                if (c == card)
                    removableCard = true;
                else
                    addedCards[i++] = c;
            }

            if (removableCard)
            {
                cards = addedCards;
                return card;
            }

            return null;
        }

        public override string ToString()
        {
            string converted = "|";
            string colon = "";
            foreach (Card temp in cards)
            {
                converted += colon + temp.ToString();
                colon = "][";
            }
            converted += "|";
            Console.WriteLine();
            return converted;
        }
    }

    class Program
    {
       // private static Hand[] hand;
       // private static Deck deck;

        public static void Main(string[] args)
        {
            //creating players(hands)
            Hand[] hand = new Hand[4];
            for (int i = 0; i < 4; i++)
                hand[i] = new Hand();

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            Console.WriteLine("Assignment 3: Deck of Cards");
            Console.WriteLine("___________________________");

            Deck deck = new Deck();
            Console.WriteLine("New Deck:" + deck);

            deck.Shuffle();
            Console.WriteLine("Shuffled Deck:" + deck);

            deck.Cut();
            Console.WriteLine("Cut Shuffled Deck:" + deck);

            
            for(int i = 0; i < 5; i++)
                 foreach(Hand h in hand)
                 {
                     Card card = deck.DealCard();
                     h.AddCard(card);
                 }
            Console.WriteLine();
             Console.WriteLine("Players with Cards:");
             foreach(Hand h in hand)
                 Console.WriteLine($"{h} \nPlayer has {h.Size()} Cards");
                 
             Console.WriteLine("Cards in Deck: \n" + deck);
            Console.WriteLine();
        }
    }
}
