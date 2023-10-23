namespace Day21
{
    internal class Character
    {
        readonly List<(int cost, int dmg, int def)> weapons = new()
        {
            (8, 4, 0),              // dagger
            (10, 5, 0),             // shortsword
            (25, 6, 0),             // warhammer
            (40, 7, 0),             // longsword
            (74, 8, 0)              // greataxe
        };

        readonly List<(int cost, int dmg, int def)> armors = new()
        {
            (0, 0, 0),              // no armor
            (13, 0 ,1),             // leather
            (31, 0, 2),             // chainmail
            (53, 0, 3),             // splintmail
            (75, 0, 4),             // bandedmail
            (102, 0, 5)             // platemail
        };

        readonly List<(int cost, int dmg, int def)> rings = new()
        {
            (0, 0, 0),              // no ring
            (25, 1, 0),             // damage +1
            (50, 2, 0),             // damage +2
            (100, 3, 0),            // damage +3
            (20, 0, 1),             // defense +1
            (40, 0, 2),             // defense +2
            (80, 0, 3)              // defense +3
        };

        public int HP { get; set; }
        public int Defense { get; set; }
        public int Damage { get; set; }
        public int GoldSpent { get; set; }
        public bool ValidEquipment { get; }

        // init character with specs (for boss and testing)
        public Character(int hp, int damage, int armor)
        {
            HP = hp;
            Defense = armor;
            Damage = damage;
            GoldSpent = 0;
        }

        // init character with equipment combination number
        public Character(int hp, int combo)
        {
            HP = hp;
            ValidEquipment = false;

            string comboString = combo.ToString("D4");
            int weapon = comboString[0] - '0';
            int armor = comboString[1] - '0';
            int ring1 = comboString[2] - '0';
            int ring2 = comboString[3] - '0';

            if (weapon < 5)
            {
                Damage += weapons[weapon].dmg;
                GoldSpent += weapons[weapon].cost;
            }
            else
                return;

            if (armor < 6)
            {
                Defense += armors[armor].def;
                GoldSpent += armors[armor].cost;
            }
            else
                return;

            if (ring1 < 7)
            {
                Damage += rings[ring1].dmg;
                Defense += rings[ring1].def;
                GoldSpent += rings[ring1].cost;
            }
            else
                return;

            if (ring2 < 7)
            {
                Damage += rings[ring2].dmg;
                Defense += rings[ring2].def;
                GoldSpent += rings[ring2].cost;
            }
            else
                return;

            if (ring1 != ring2)
            {
                ValidEquipment = true;
            }
            else
            {
                if (ring1 == 0 && ring2 == 0)
                    ValidEquipment = true;
                else
                    ValidEquipment = false;
            }
        }
    }
}
