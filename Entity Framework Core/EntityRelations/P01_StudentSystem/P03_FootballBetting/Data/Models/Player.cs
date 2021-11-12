using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P03_FootballBetting.Data.Models
{
    public class Player
    {
        public Player()
        {
            this.PlayerStatistics = new HashSet<PlayerStatistic>();
        }
        [Key]
        public int PlayerId { get; set; }

        public string Name { get; set; }

        // whose team number
        public int SquadNumber { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }

        // is live or dead
        public bool IsInjured { get; set; }


        // collection of player statistics
        public ICollection<PlayerStatistic> PlayerStatistics { get; set; }
    }
}
