using System;
using System.Collections.Generic;
using System.Text;

namespace RedeDePetri
{
    class Token
    {
        public TokenType type;

        public Token(TokenType type = TokenType.DEFAULT)
        {
            this.type = type;
        }
    }
}
