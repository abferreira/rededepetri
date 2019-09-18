using System;
using System.Collections.Generic;
using System.Text;

namespace RedeDePetri
{
    class Place
    {
        public int id;
        public string label;
        public List<Token> tokens;
        public bool isTransition;

        public Place(string label = "")
        {
            this.id = Subnet.GetNextID();
            this.label = label;
            this.tokens = new List<Token>();
            this.isTransition = false;
        }

        public Place(string label, bool isTransition)
        {
            this.id = Subnet.GetNextID();
            this.label = label;
            this.tokens = new List<Token>();
            this.isTransition = isTransition;
        }

        public int GetTokenCount()
        {
            return tokens.Count;
        }

        public void RemoveTokens(int amount)
        {
            if(tokens.Count >= amount)
            {
                for (int i = 0; i < amount; i++)
                {
                    tokens.RemoveAt(0);
                }
            }
        }

        public void AddTokens(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                tokens.Add(new Token());
            }
        }

        public string Info()
        {
            string result = this.isTransition ? $"Transition {label}\n" : $"Place {label}\n";
            result += $"Id: {this.id}\n";
            if (!this.isTransition)
            {
                result += $"Tokens: {GetTokenCount()}\n";
            }
            return result;
        }
    }
}
