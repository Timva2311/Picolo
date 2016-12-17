//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using System.Text.RegularExpressions;
    public partial class Rule
    {

        public static readonly int CALIENTE_TYPE = 7;
        public static readonly int CHAMPION_TYPE = 11;
        public static readonly int CONSTRAINT_TYPE = 3;
        public static readonly int CULSEC_TYPE = 5;
        public static readonly int CYCLE_SEEN = 1;
        public static readonly int CYCLE_UNSEEN = 0;
        public static readonly int GAME_TYPE = 4;
        public static readonly string GULP_SYMBOL = "$";
        public static readonly int INSTANT_TYPE = 1;
        public static readonly int KAMOULOX_TYPE = 15;
        public static readonly int LONG_TYPE = 2;
        public static readonly int ONE_FOR_ALL_TYPE = 10;
        public static readonly int OWNED_TYPE = 6;
        public static readonly int PUNITION_TYPE = 13;
        public static readonly int QUESTION_TYPE = 8;
        public static readonly string TEAM_SYMBOL = "%t";
        public static readonly string PLAYER_SYMBOL = "%s";
        public static readonly int THEME_TYPE = 12;
        public static readonly int VIOLIN_TYPE = 9;
        public static readonly int WYR_TYPE = 14;
        public int CycleState { get; set; }

        public string GeneratedText
        {
            get
            {
                if (string.IsNullOrEmpty(_generatedText))
                {
                    GenerateText();
                }
                return _generatedText;
            }
            set { _generatedText = value; }
        }

        private string _generatedText;

        public int Id { get; set; }
        public string rule_start { get; set; }
        public string rule_end { get; set; }
        public string String { get; set; }
        public int Type { get; set; }
        public int Names { get; set; }
        public List<Player> Players { get; set; }
        public List<Player> RuledPlayers { get; set; }
        public int Turn { get; set; }

        public void GenerateText()
        {
            var playerIndex = 0;
            var playerCount = 0;
            var currentText = String;
            List<Player> RuledPlayers = new List<Player>();
            while (playerIndex != -1)
            {
                playerIndex = currentText.IndexOf(Rule.PLAYER_SYMBOL, playerIndex, StringComparison.Ordinal);
                if (playerIndex == -1) continue;
                var regex = new Regex(Regex.Escape(Rule.PLAYER_SYMBOL));
                currentText = regex.Replace(currentText, Players[playerCount].Name, 1);
                RuledPlayers.Add(Players[playerCount]);
                playerCount++;
            }

            var gulpIndex = 0;
            var rand = new Random();
            while (gulpIndex != -1)
            {
                gulpIndex = currentText.IndexOf(Rule.GULP_SYMBOL, gulpIndex, StringComparison.Ordinal);
                if (gulpIndex == -1) continue;
                var regex = new Regex(Regex.Escape(Rule.GULP_SYMBOL));
                currentText = regex.Replace(currentText, $"{rand.Next((Max_Gulp() - Min_Gulp()) + 1) + Min_Gulp()}", 1);
            }
            GeneratedText = currentText;
            this.RuledPlayers = RuledPlayers;
        }
        public void GenerateTextForRule(List<Player> Players)
        {
            var playerIndex = 0;
            var playerCount = 0;
            var currentText = String;
            while (playerIndex != -1)
            {
                playerIndex = currentText.IndexOf(Rule.PLAYER_SYMBOL, playerIndex, StringComparison.Ordinal);
                if (playerIndex == -1) continue;
                var regex = new Regex(Regex.Escape(Rule.PLAYER_SYMBOL));
                currentText = regex.Replace(currentText, Players[playerCount].Name, 1);
                playerCount++;
            }

            var gulpIndex = 0;
            var rand = new Random();
            while (gulpIndex != -1)
            {
                gulpIndex = currentText.IndexOf(Rule.GULP_SYMBOL, gulpIndex, StringComparison.Ordinal);
                if (gulpIndex == -1) continue;
                var regex = new Regex(Regex.Escape(Rule.GULP_SYMBOL));
                currentText = regex.Replace(currentText, $"{rand.Next((Max_Gulp() - Min_Gulp()) + 1) + Min_Gulp()}", 1);
            }
            GeneratedText = currentText;
        }
        public string TypeString()
        {
            if (!string.IsNullOrEmpty(rule_end) || !string.IsNullOrEmpty(rule_start))
            {
                return "rule";
            }
            switch (Type)
            {
                case 4:
                case 14:
                    return "Game";
                case 5:
                    return "Bottoms up";
                default:
                    return "";
            }
        }
        public int Max_Gulp()
        {
            switch (Type)
            {
                case 2:
                case 3:
                    return 3;
                case 4:
                case 14:
                    return 5;
                default:
                    return 5;
            }
        }
        public int Min_Gulp()
        {
            switch (Type)
            {
                case 2:
                case 3:
                    return 1;
                case 4:
                case 14:
                    return 2;
                default:
                    return 5;
            }
        }
    }
}
