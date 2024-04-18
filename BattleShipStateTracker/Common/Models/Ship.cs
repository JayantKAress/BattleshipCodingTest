using System.Collections.Generic;

namespace BattleShipStateTracker.Common.Models
{
    public class Ship
    {
        public string Name { get; set; }
        public List<(int, int)> Positions { get; set; }
        public Ship(string name)
        {
            Name = name;
            Positions = new List<(int, int)>();
        }
    }

    public class ShipCordinates
    {
        public int Xcoordinate { get; set; }
        public int Ycoordinate { get; set; }
    }
}
