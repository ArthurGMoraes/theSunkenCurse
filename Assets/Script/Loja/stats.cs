public class Stats
{
    public int Dinheiro { get; private set; }
    public double JetpackSpeed { get; private set; }
    public double JetpackFuel { get; private set; }
    public double Health { get; private set; }
    public double Damage { get; private set; }

    public Stats()
    {
        Dinheiro = 0;
        JetpackSpeed = 1;
        JetpackFuel = 1;
        Health = 1;
        Damage = 1;
    }

    public void SetDinheiro(int dinheiro)
    {
        Dinheiro = dinheiro;
    }

    public void SetJetpackSpeed(double jetpackSpeed)
    {
        JetpackSpeed = jetpackSpeed;
    }

    public void SetJetpackFuel(double jetpackFuel)
    {
        JetpackFuel = jetpackFuel;
    }

    public void SetHealth(double health)
    {
        Health = health;
    }

    public void SetDamage(double damage)
    {
        Damage = damage;
    }

    public override string ToString()
    {
        return $"Dinheiro: {Dinheiro} Jetpack Speed: {JetpackSpeed} Jetpack Fuel: {JetpackFuel} Health: {Health} Damage: {Damage}";
    }
}
