using BL;
using DAL;
using Domain;
using DrinkingGame._Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Picolo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRuleManager _mgr;

        public HomeController()
        {
            _mgr = new RuleManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Game(FormCollection form)
        {
            var names = form["name"].Split(char.Parse(","));
            var sex = form["sex"].Split(char.Parse(","));

            var players = new List<Player>();
            for (int i = 0; i < names.Length; i++)
            {
                if (string.IsNullOrEmpty(names[i]))
                {
                    continue;
                }
                var player = new Player()
                {
                    Name = names[i],
                    Sex = (Sex)Enum.Parse(typeof(Sex), sex[i])
                };
                players.Add(player);
            }


            int count = _mgr.CountRules();
            var rand = new Random();
            var good = false;
            Rule rule = null;
            while (!good)
            {
                var random = rand.Next(count);
                rule = _mgr.FindRule(random);
                if (!string.IsNullOrEmpty(rule.rule_start))
                {
                    rule.Turn = 0;
                    Session["Rules"] = new List<Rule> {rule};
                    good = true;
                }
                else if (string.IsNullOrEmpty(rule.rule_end))
                {
                    Session["Rules"] = new List<Rule>();
                    good = true;
                }

            }
            Rule displayRule = (rule);
            players.Shuffle();
            Session["players"] = players;
            displayRule.Players = players;
            displayRule.GenerateText();
            return View(displayRule);
        }

        public ActionResult GameResult()
        {
            int count = _mgr.CountRules();
            var players = (List<Player>)Session["players"];
            var rand = new Random();
            var good = false;
            var isEndRule = false;
            List<Rule> activeRules = (List<Rule>)Session["Rules"];
            List<Player> ruledPlayers = new List<Player>();
            Rule rule = null;
            foreach(Rule activeRule in activeRules)
            {
                if (activeRule.Type != 99)
                {
                    activeRule.Turn++;
                    if (activeRule.Turn > Rule.LONG_TYPE && activeRule.Turn < Rule.CULSEC_TYPE)
                    {
                        if (rand.Next(5) == 1)
                        {
                            rule = _mgr.FindEndrule(activeRule.rule_start);
                            good = true;
                            isEndRule = true;
                            activeRule.Type = 99;
                            ruledPlayers = activeRule.RuledPlayers;
                        }
                    }
                    else if (activeRule.Turn == Rule.CULSEC_TYPE)
                    {
                        rule = _mgr.FindEndrule(activeRule.rule_start);
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
                rule = _mgr.FindRule(random);
                if (!string.IsNullOrEmpty(rule.rule_start))
                {
                    activeRules.Add(rule);
                }
                if (string.IsNullOrEmpty(rule.rule_end))
                    good = true;
            }

            Session["Rules"] = activeRules;
            Rule displayRule = (rule);
            players.Shuffle();
            Session["players"] = players;
            displayRule.Players = players;
            if (isEndRule)
            {
                displayRule.GenerateTextForRule(ruledPlayers);
            }
            else
            {
                displayRule.GenerateText();
            }
            return View(displayRule);
        }

        public ActionResult Rules()
        {
            return View();
        }
    }
}