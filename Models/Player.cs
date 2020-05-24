using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alex.Models
{
    public class Player : GameObject
    {
        public int Lvl { get; set; }
        public int Strength { get; set; }
        public int Hp { get; set; }
        public int CurrentHp { get; set; }
    }
}
