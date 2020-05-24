using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alex.Models;

namespace Alex
{
    class Program
    {
        static void Main(string[] args)
        {
            // Defining variables
            int value = 10;
            int amountOfMobs = 10;
            int currentMobs = 0;
            List<GameObject> objects = new List<GameObject>();
            objects.Add(SpawnPlayer());

            while (amountOfMobs != currentMobs)
            {
                objects.Add(SpawnMob(objects));
                currentMobs++;
            }
            Player player = (Player)objects.FirstOrDefault(x => x.GetType() == typeof(Player));

            Console.WriteLine("Press any key to start...");
            // Game starts here
            while (true)
            {
                // Waiting for key to be pressed
                ConsoleKeyInfo keyInfoPressed = Console.ReadKey();
                Commands(player, objects, keyInfoPressed, value);

                // Top text
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Clear();
                Console.Write("X = " + player.xCoord + " ", Console.ForegroundColor);
                Console.WriteLine("Y = " + player.yCoord + " ", Console.ForegroundColor);
                Console.WriteLine("Name " + player.Name + " ", Console.ForegroundColor);
                Console.WriteLine("LVL " + player.Lvl + " ", Console.ForegroundColor);
                Console.Write("HP " + player.Hp, Console.ForegroundColor);
                Console.WriteLine("/" + player.CurrentHp + " ", Console.ForegroundColor);
                Console.WriteLine("Strength " + player.Strength + " ", Console.ForegroundColor);


                for (int row = 1; row <= value; row++)
                {
                    for (int drawLine = 1; drawLine <= value; drawLine++)
                    {
                        if (objects.Find(x => x.yCoord == -row && x.xCoord == drawLine) != null)
                        {
                            var ob = objects.Find(x => x.yCoord == -row && x.xCoord == drawLine);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" ");
                            if (ob.GetType() == typeof(Player))
                            {
                                Console.Write("@", Console.ForegroundColor);
                            }
                            else
                            {
                                Console.Write("M", Console.ForegroundColor);
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" ", Console.ForegroundColor);
                            Console.Write("-", Console.ForegroundColor);
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine("Use arrow keys to move");
            }
        }
        static void Attack()
        {
            Console.WriteLine();
            Console.WriteLine("Would you like to attack?");
            Console.ReadLine();
        }
        // Player setup
        static Player SpawnPlayer()
        {
            return new Player
            {
                Name = "Player",
                Hp = 10,
                CurrentHp = 10,
                Lvl = 1,
                Strength = 10,
                xCoord = 1,
                yCoord = -1
            };
        }
        // Mob Spawner
        static Mob SpawnMob(List<GameObject> objects, int level = 1, int hp = 10, int strength = 10)
        {
            var valid = false;
            var mob = new Mob();
            while (!valid)
            {
                int x = NumberGenerator(10, 1);
                int y = NumberGenerator(-1, -10);
                bool positionTaken = false;
                foreach (var ob in objects)
                {
                    if(ob.xCoord == x && ob.yCoord == y)
                    {
                        positionTaken = true;
                    }
                }
                if (!positionTaken)
                {
                    valid = true;
                    mob.Name = "Mob";
                    mob.Hp = hp;
                    mob.CurrentHp = 10;
                    mob.Lvl = level;
                    mob.xCoord = x;
                    mob.yCoord = y;
                    mob.Strength = strength;
                }
            }
            return mob;
        }
        static int NumberGenerator(int max, int min)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        static bool ObjectsCollide(List<GameObject> objects, Enums.Direction direction, GameObject ob)
        {
            if (direction == Enums.Direction.Down)
            {
                if (objects.FirstOrDefault(x => x.yCoord == (ob.yCoord - 1) && x.xCoord == ob.xCoord) != null)
                {
                    return true;
                }
            }
            if (direction == Enums.Direction.Up)
            {
                if (objects.FirstOrDefault(x => x.yCoord == (ob.yCoord + 1) && x.xCoord == ob.xCoord) != null)
                {
                    return true;
                }
            }
            if (direction == Enums.Direction.Left)

            {
                if (objects.FirstOrDefault(x => x.xCoord == (ob.xCoord - 1) && x.yCoord == ob.yCoord) != null)
                {
                    return true;
                }
            }
            if (direction == Enums.Direction.Right)
            {
                if (objects.FirstOrDefault(x => x.xCoord == (ob.xCoord + 1) && x.yCoord == ob.yCoord) != null)
                {
                    return true;
                }
            }
            return false;
        }
        static void Commands(Player player, List<GameObject> objects, ConsoleKeyInfo keyInfoPressed, int value)
        {
            switch (keyInfoPressed.Key)
            {
                case ConsoleKey.UpArrow:
                    if (player.yCoord < -1)
                    {
                        if (ObjectsCollide(objects, Enums.Direction.Up, player))
                        {
                            Attack();
                        }
                        else
                        {
                            player.yCoord++;
                        }
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (player.yCoord > -value)
                    {
                        if (ObjectsCollide(objects, Enums.Direction.Down, player))
                        {
                            Attack();
                        }
                        else
                        {
                            player.yCoord--;
                        }
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (player.xCoord < value)
                    {
                        if (ObjectsCollide(objects, Enums.Direction.Right, player))
                        {
                            Attack();
                        }
                        else
                        {
                            player.xCoord++;
                        }
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (player.xCoord > 1)
                    {
                        if (ObjectsCollide(objects, Enums.Direction.Left, player))
                        {
                            Attack();
                        }
                        else
                        {
                            player.xCoord--;
                        }
                    }
                    break;
                case ConsoleKey.Q:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
