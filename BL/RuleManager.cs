using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain;

namespace BL
{
    public class RuleManager : IRuleManager
    {
        private readonly Drinking_GameEntities entities;


        public RuleManager (List<Player> players)
        {
            entities = new Drinking_GameEntities();
        }
        public RuleManager ()
        {
            entities = new Drinking_GameEntities();
        }

        public int CountRules()
        {
           return entities.Rules.Count();
        }

        public Rule FindEndrule(string end)
        {
            return entities.Rules.Where(r => r.rule_end.Equals(end)).First();
        }

        public Rule FindRule(int id)
        {
            return entities.Rules.Find(id);
        }

        public Rule ExecuteGameLogic(ref List<Player> activePlayers, ref List<Rule> activeRules)
        {
            
            int count = CountRules();
            var rand = new Random();
            var good = false;
            Rule rule = null;
            var isEndRule = false;
            List<Player> ruledPlayers = new List<Player>();

            foreach (Rule activeRule in activeRules)
            {
                if (activeRule.Type != 99)
                {
                    activeRule.Turn++;
                    if (activeRule.Turn > Rule.LONG_TYPE && activeRule.Turn < Rule.CULSEC_TYPE)
                    {
                        if (rand.Next(5) == 1)
                        {
                            rule = FindEndrule(activeRule.rule_start);
                            good = true;
                            isEndRule = true;
                            activeRule.Type = 99;
                            ruledPlayers = activeRule.RuledPlayers;
                        }
                    }
                    else if (activeRule.Turn == Rule.CULSEC_TYPE)
                    {
                        rule = FindEndrule(activeRule.rule_start);
                        good = true;
                        isEndRule = true;
                        activeRule.Type = 99;
                        ruledPlayers = activeRule.RuledPlayers;
                    }
                }
            }
            while (!good)
            {
                var random = rand.Next(count);
                rule = FindRule(random);
                if (!string.IsNullOrEmpty(rule.rule_start))
                {
                    rule.Turn = 0;
                    activeRules.Add(rule);
                }
                if (string.IsNullOrEmpty(rule.rule_end))
                    good = true;
            }
            Rule displayRule = rule;
            
            displayRule.Players = activePlayers;
            if (isEndRule)
            {
                displayRule.GenerateTextForRule(ruledPlayers);
            }
            else
            {
                displayRule.GenerateText();
            }
            return displayRule;
        }
    }
}
