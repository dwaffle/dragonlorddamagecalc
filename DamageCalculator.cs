using System;

public class DamageCalculator
{
	int attackPower;
	int mp;
	bool hasDeathNecklace;

	public int CalculateMaxDamage(int attackPower, bool hasDeathNecklace)
    {
		if(hasDeathNecklace == true)
        {
			attackPower + 10;
        }
		 
		if((attackPower %2) != 0)
        {
			attackPower - 1;
        }

		return Math.Floor((attackPower - 100) / 2);
    }

	public int CalculateMinDamage(int attackPower, bool hasDeathNecklace)
    {
		return Math.Floor((CalculateMaxDamage(attackPower, hasDeathNecklace)) / 2);
    }

	public int CalculateHealmores(int mp)
    {
		return Math.Floor(mp / 8);
    }
}
