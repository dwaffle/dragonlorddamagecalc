using System;

public class DamageCalculator
{
	public int CalculateMaxDamage(int attackPower, bool? hasDeathNecklace)
    {
		if (attackPower < 100) return 0;
		if(hasDeathNecklace == true)
        {
			attackPower += 10;
        }
		 
		if((attackPower %2) != 0)
        {
			attackPower--;
        }

		return (int)Math.Floor((double)(attackPower - 100) / 2);
    }

	public int CalculateAvgDamage(int attackPower, bool? hasDeathNecklace)
    {
		if (attackPower < 100) return 0;
		return (int)Math.Floor((double)(CalculateMaxDamage(attackPower, hasDeathNecklace) + CalculateMinDamage(attackPower, hasDeathNecklace))/2);
    }

	public int CalculateMinDamage(int attackPower, bool? hasDeathNecklace)
    {
		if (attackPower < 100) return 0;
		if(hasDeathNecklace == true)
        {
			attackPower += 10;
        }

		if(attackPower %2 != 0)
        {
			attackPower--;
        }
		return (int)Math.Floor((double)((attackPower -100) /4));
    }

	public int CalculateHealmores(int mp)
    {
		return (int)Math.Floor((double)mp / 8);
    }
}
