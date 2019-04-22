class Ability
{
    public Ability(int cooldown, Action ab)
    {
        CoolDown = cooldown;
        TheAbility = ab;
    }

    public int CoolDown { get; set; }

    public Action TheAbility;
}


class Warrior : CharacterClass
{
    public Warrior()
    {
        // Set the new Abilities
        AbilityOne = new Ability(3, Smash);
        AbilityTwo = new Ability(7, ShieldBlock);

        AbilityOne.TheAbility(); // Will run Smash() method
    }

    private void Smash()
    {
        // Ability 1
    }

    private void ShieldBlock()
    {
        // Ability 2
    }
}
selectedClass.AbilityOne.Trigger();
class Ability
{
    public Ability(int cooldown, System.Action _abilityMethod)
    {
        CoolDown = cooldown;
        abilityMethod = _abilityMethod;
    }

    public int CoolDown { get; set; }

    public void Trigger()
    {
        if( abilityMethod != null )
        {
            abilityMethod();
        }
    }

    private System.Action abilityMethod;
}