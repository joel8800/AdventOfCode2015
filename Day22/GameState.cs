namespace Day22
{
    public class GameState
    {
        private class State
        {
            // null = no winner yet, true = player win
            public bool? PlayerWins;
            public int PlayerHP { get; set; }
            public int PlayerMana { get; set; }
            public int BossHP { get; set; }
            public int BossDamage { get; set; }
            public int ShieldEffect { get; set; }
            public int PoisonEffect { get; set; }
            public int RechargeEffect { get; set; }
            public int ManaSpent { get; set; }

            public State() { }
            
            public State(int playerHP, int playerMana, int bossHP, int bossDamage)
            {
                PlayerWins = null;
                PlayerHP = playerHP;
                PlayerMana = playerMana;
                BossHP = bossHP;
                BossDamage = bossDamage;
                ShieldEffect = 0;
                PoisonEffect = 0;
                RechargeEffect = 0;
                ManaSpent = 0;
            }

            public State Duplicate() => new()
            {
                PlayerWins = this.PlayerWins,
                PlayerHP = this.PlayerHP,
                PlayerMana = this.PlayerMana,
                BossHP = this.BossHP,
                BossDamage = this.BossDamage,
                ShieldEffect = this.ShieldEffect,
                PoisonEffect = this.PoisonEffect,
                RechargeEffect = this.RechargeEffect,
                ManaSpent = this.ManaSpent
            };
        }

        public int Battle(bool isPart2)
        {
            const int playerHP = 50;
            const int playerMana = 500;
            const int bossHP = 58;
            const int bossDamage = 9;

            State init = new State(playerHP, playerMana, bossHP, bossDamage);

            PriorityQueue<State, int> states = new();
            states.Enqueue(init, 0);

            while (states.Peek().PlayerWins != true)
            {
                State current = states.Dequeue();
                if (current.PlayerWins == false)
                    continue;
                IEnumerable<State> nextStates = GetAllNextStates(current, isPart2);
                states.EnqueueRange(nextStates.Select(e => (e, e.ManaSpent)));
            }

            return states.Peek().ManaSpent;
        }

        private static IEnumerable<State> GetAllNextStates(State current, bool isPart2)
        {
            if (isPart2)
                current.PlayerHP--;
            if (current.PlayerHP < 1)
                yield break;

            // new states for each spell
            State missile = current.Duplicate();
            missile.BossHP -= 4;
            missile.PlayerMana -= 53;
            missile.ManaSpent += 53;
            
            State drain = current.Duplicate();
            drain.BossHP -= 2;
            drain.PlayerHP += 2;
            drain.PlayerMana -= 73;
            drain.ManaSpent += 73;

            State shield = current.Duplicate();
            shield.ShieldEffect = 6;
            shield.PlayerMana -= 113;
            shield.ManaSpent += 113;

            State poison = current.Duplicate();
            poison.PoisonEffect = 6;
            poison.PlayerMana -= 173;
            poison.ManaSpent += 173;

            State recharge = current.Duplicate();
            recharge.RechargeEffect = 5;
            recharge.PlayerMana -= 229;
            recharge.ManaSpent += 229;

            // check results of next round
            FightNextRound(drain, isPart2);
            FightNextRound(shield, isPart2);
            FightNextRound(poison, isPart2);
            FightNextRound(recharge, isPart2);
            FightNextRound(missile, isPart2);

            if (missile.PlayerMana >= 0)
                yield return missile;
            if (drain.PlayerMana >= 0)
                yield return drain;
            if (shield.PlayerMana >= 0 && current.ShieldEffect == 0)
                yield return shield;
            if (poison.PlayerMana >= 0 && current.PoisonEffect == 0)
                yield return poison;
            if (recharge.PlayerMana >= 0 && current.RechargeEffect == 0)
                yield return recharge;
        }

        private static void FightNextRound(State current, bool isPart2)
        {
            // Handle spell effect timers

            // do poison spell first because it can end the fight
            if (current.PoisonEffect > 0)
            {
                current.PoisonEffect--;
                current.BossHP -= 3;

                // check if poison killed boss
                if (current.BossHP < 1)
                {
                    current.PlayerWins = true;
                    return;
                }
            }

            if (current.ShieldEffect > 0)
            {
                current.ShieldEffect--;
                if (current.BossDamage <= 7)
                    current.PlayerHP--;
                else
                    current.PlayerHP -= current.BossDamage - 7;
            }
            else
                current.PlayerHP -= current.BossDamage;

            // check if boss attack killed player
            if (current.PlayerHP < 1)
            {
                current.PlayerWins = false;
                return;
            }

            if (current.RechargeEffect > 0)
            {
                current.RechargeEffect--;
                current.PlayerMana += 101;
            }

            // end of Boss' turn, start of Player's turn, apply effects

            if (isPart2)
            {
                current.PlayerHP--;
                if (current.PlayerHP < 1)
                {
                    current.PlayerWins = false;
                    return;
                }
            }

            if (current.ShieldEffect > 0)
                current.ShieldEffect--;

            if (current.PoisonEffect > 0)
            {
                current.PoisonEffect--;
                current.BossHP -= 3;
            }

            if (current.RechargeEffect > 0)
            {
                current.RechargeEffect--;
                current.PlayerMana += 101;
            }

            // check if poison killed boss
            if (current.BossHP < 1)
                current.PlayerWins = true;
        }
    }
}
