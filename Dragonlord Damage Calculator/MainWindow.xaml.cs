using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace Dragonlord_Damage_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            DamageCalculator damageCalculator = new DamageCalculator();
            if(isNumber(Mana.Text))
            {
                HealmoresRequired.Text = $"You have {damageCalculator.CalculateHealmores(int.Parse(Mana.Text))} HEALMORES.  " + 
                    "You need to put in attack power to find your minimum, maximum and average";
            }
            if(isNumber(AttackPower.Text))
            {
                MinDamage.Text = damageCalculator.CalculateMinDamage(int.Parse(AttackPower.Text), YesDeathNecklace.IsChecked).ToString();
                MaxDamage.Text = damageCalculator.CalculateMaxDamage(int.Parse(AttackPower.Text), YesDeathNecklace.IsChecked).ToString();
                AvgDamage.Text = damageCalculator.CalculateAvgDamage(int.Parse(AttackPower.Text), YesDeathNecklace.IsChecked).ToString();
            }
            if(isNumber(Mana.Text) && isNumber(AttackPower.Text))
            {
                int totalAttackPower = int.Parse(AttackPower.Text);
                int maxAttackDamage = damageCalculator.CalculateMaxDamage(totalAttackPower, YesDeathNecklace.IsChecked);
                int avgAttackDamage = damageCalculator.CalculateAvgDamage(totalAttackPower, YesDeathNecklace.IsChecked);
                int minAttackDamage = damageCalculator.CalculateMinDamage(totalAttackPower, YesDeathNecklace.IsChecked);
                int totalHealmores = damageCalculator.CalculateHealmores(int.Parse(Mana.Text));
                if (totalAttackPower >= 102)
                {
                    HealmoresRequired.Text = $"You have {totalHealmores} HEALMORES.  You will need at least {(maxAttackDamage <= 0 ? 0 : 150 / maxAttackDamage)} attacks, " +
                        $"an average of {(avgAttackDamage <= 0 ? 0 : 158 / avgAttackDamage)}, " +
                        $"and a maximum of {(minAttackDamage <= 0 ? 0 : 165 / minAttackDamage)}";
                } else
                {
                    HealmoresRequired.Text = $"You have {totalHealmores} HEALMORES.  You need at least 102 attack power to damage the Dragonlord";
                }
                }
        }
        
        private void SimButton_Click(object sender, RoutedEventArgs e)
        {
            int totalWins = 0;
            if(isNumber(MaxHealth.Text) && isNumber(Mana.Text) && isNumber(AttackPower.Text))
            {
                DLFightSimulator fightSim = new DLFightSimulator();
                Player player = new Player { attackPower = int.Parse(AttackPower.Text), maxMana = int.Parse(Mana.Text),
                    hasDeathNecklace = (bool)YesDeathNecklace.IsChecked,
                    playerMaxHealth = int.Parse(MaxHealth.Text),
                };
                //If the player doesn't supply a current health, assume they have full health.
                if (isNumber(CurrentHealth.Text)) player.playerCurrentHealth = int.Parse(CurrentHealth.Text);
                else player.playerCurrentHealth = player.playerMaxHealth;

                for (int i = 0; i < 10000; i++)
                {
                    Player player2 = new Player();
                    player2.attackPower = player.attackPower;
                    player2.hasDeathNecklace = player.hasDeathNecklace;
                    player2.maxMana = player.maxMana;
                    player2.playerMaxHealth = player.playerMaxHealth;
                    player2.playerCurrentHealth = player.playerCurrentHealth;
                    fightSim.SimulateFight(player2);
                    if (player2.hasWon) totalWins++;
                }
                fightSim.SimulateFight(player);
                SimBox.Text = $"You took {player.turnsTaken + 1} turns and {(player.hasWon ? "won" : "lost")}" +
                    $"\n in {player.attacksMade} total attacks." +
                    $"\n";
                
                SimBox.Text += ($"\nOut of 10000 fights, you won {(float)totalWins/100}%");
            } else
            {
                SimBox.Text = "";
            }
        }

        bool isNumber(string number)
        {
            int temp;
            return int.TryParse(number, out temp);
        }

        
    }
}
