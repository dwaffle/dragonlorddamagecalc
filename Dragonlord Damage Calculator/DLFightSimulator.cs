using System;

namespace Dragonlord_Damage_Calculator
{
    public class Player 
    {
        public bool hasDeathNecklace;
        public int attackPower;
        public bool hasWon;
        public int maxMana;
        public int playerMaxHealth;
        public int playerCurrentHealth;
        public int turnsTaken;
        public int attacksMade;
        public int minDamage;
        public int maxDamage;
    }

    public class DLFightSimulator
    {
        public void SimulateFight(Player player)
        {
            Random rng = new Random();
            DamageCalculator dmg = new DamageCalculator();
            int dragonlordHealth = rng.Next(150, 166);
            player.minDamage = dmg.CalculateMinDamage(player.attackPower, player.hasDeathNecklace);
            player.maxDamage = dmg.CalculateMaxDamage(player.attackPower, player.hasDeathNecklace);
            if (player.hasDeathNecklace) player.playerMaxHealth -= (int)Math.Floor(player.playerMaxHealth * 0.25);
            while (true)
            {
                PlayersTurn(player, ref dragonlordHealth);
                if(dragonlordHealth <= 0 )
                {
                    player.hasWon = true;
                    break;
                }
                  player.turnsTaken++;
                //Represents DL's attack.
                player.playerCurrentHealth -= rng.Next(38, 49);
                if (player.playerCurrentHealth <= 0)
                {
                    player.hasWon = false;
                    break;
                }
                
            }
        }

        public void PlayersTurn(Player player, ref int dragonlordHealth)
        {
            Random rng = new Random();
            //If the player can attack safely, they do.  Otherwise, they cast HEALMORE.  They also attack if they're out of HEALMORES.
            if (player.playerCurrentHealth > 49 || (player.playerMaxHealth < 49 && player.maxMana < 8))
            {
                dragonlordHealth -= rng.Next(player.minDamage, player.maxDamage + 1);
                player.attacksMade++;
            }
            else
            {
                if (player.maxMana >= 8)
                {
                    player.maxMana -= 8;
                    player.playerCurrentHealth += rng.Next((85 > player.playerMaxHealth ? 85 : player.playerMaxHealth), 
                        (101 > player.playerMaxHealth ? 101: player.playerMaxHealth));
                    //Cant heal more than your max HP!
                    if (player.playerCurrentHealth > player.playerMaxHealth) player.playerCurrentHealth = player.playerMaxHealth;
                }
            }
        }
    }
}