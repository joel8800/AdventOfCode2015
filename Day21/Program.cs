using AoCUtils;
using Day21;

Console.WriteLine("Day21: RPG Simulator 20XX");

int bossHP = 109;
int bossDamage = 8;
int bossArmor = 2;

int plyrHP = 100;
int minGold = Int32.MaxValue;
int maxGold = Int32.MinValue;


for (int i = 0; i < 4576; i++)
{
    Character boss = new(bossHP, bossDamage, bossArmor);
    Character player = new(plyrHP, i);

    if (player.ValidEquipment == false)
        continue;

    Console.Write($"Equipment {i}, gold spent {player.GoldSpent}: ");

    if (Battle(player, boss) == true)
    {
        Console.WriteLine("Player wins");
        if (player.GoldSpent < minGold)
            minGold = player.GoldSpent;
    }
    else
        Console.WriteLine("Boss wins");

}

for (int i = 0; i < 4576; i++)
{
    Character boss = new(bossHP, bossDamage, bossArmor);
    Character player = new(plyrHP, i);

    if (player.ValidEquipment == false)
        continue;

    Console.Write($"Equipment {i}, gold spent {player.GoldSpent}: ");

    if (Battle(player, boss) == false)
    {
        Console.WriteLine("Boss wins");
        if (player.GoldSpent > maxGold)
            maxGold = player.GoldSpent;
    }
    else
        Console.WriteLine("Player wins");
}

Console.WriteLine($"Part1: {minGold}");
Console.WriteLine($"Part2: {maxGold}");


// ===========================================================================

bool Battle(Character plyr, Character boss)
{
    while (plyr.HP > 0 && boss.HP > 0)
    {
        int dmgToBoss = (plyr.Damage - boss.Defense) > 0 ? plyr.Damage - boss.Defense : 1;
        int dmgToPlyr = (boss.Damage - plyr.Defense) > 0 ? boss.Damage - plyr.Defense : 1;

        boss.HP -= dmgToBoss;
        //Console.WriteLine($"Player deals {dmgToBoss}; Boss goes down to {boss.HP} hit points");

        if (boss.HP <= 0)
            return true;

        plyr.HP -= dmgToPlyr;
        //Console.WriteLine($"Boss deals {dmgToPlyr}; Player goes down to {plyr.HP} hit points");

        if (plyr.HP <= 0)
            return false;
    }

    return false;
}