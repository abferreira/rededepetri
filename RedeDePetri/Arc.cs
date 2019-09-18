using System;
using System.Collections.Generic;
using System.Text;

namespace RedeDePetri
{
    class Arc
    {
        public Place origin;
        public Place target;
        public int weight;
        public bool isInhibitor;
        public bool isReset;

        public Arc(int weight, Place origin, Place target)
        {
            this.weight = weight;
            this.origin = origin;
            this.target = target;
        }

        public Arc(int weight, Place origin, Place target, bool isInhibitor, bool isReset)
        {
            this.weight = weight;
            this.origin = origin;
            this.target = target;
            this.isInhibitor = isInhibitor;
            this.isReset = isReset;
        }

        public bool CheckEnabled()
        {
            if (!origin.isTransition && !isReset)
            {
                if(origin.GetTokenCount() >= this.weight)
                {
                    if (this.isInhibitor)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if(this.isInhibitor)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public void Execute()
        {
            if (CheckEnabled())
            {
                if (origin.isTransition)
                {
                    target.AddTokens(this.weight);
                }
                else
                {
                    if (isReset)
                    {
                        origin.RemoveTokens(origin.GetTokenCount());
                    }
                    origin.RemoveTokens(weight);
                }
            }
        }

        public string Info()
        {
            string result = "";
            if (isReset)
            {
                result = $"Reset Arc\n";
            }else if (isInhibitor)
            {
                result = $"Inhibitor Arc\n";
            }
            else
            {
                result = $"Arc\n";
            }
            result += $"Weight: {this.weight}\n";
            result += $"Origin ID: {this.origin.label}\n";
            result += $"Target ID: {this.target.label}\n";
            return result;
        }
    }
}
