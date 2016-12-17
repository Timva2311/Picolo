using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BL
{
    public class RuleManager : IRuleManager
    {
        private readonly Drinking_GameEntities entities;

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
    }
}
