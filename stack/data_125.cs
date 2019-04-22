public abstract class Unit
{
    // This approach would use one Dictionary per Trait
    protected abstract Dictionary<int, int> MaxHpByLevel { get; }

    public int Level { get; set; } = 1;

    public int MaxHp => this.MaxHpByLevel[this.Level];
}

public class Soldier : Unit
{
    protected override Dictionary<int, int> MaxHpByLevel => new Dictionary<int, int>
    {
        [1] = 5,
        [2] = 6,
        [3] = 8
    };
}

public class Mage : Unit
{
    protected override Dictionary<int, int> MaxHpByLevel => new Dictionary<int, int>
    {
        [1] = 3,
        [2] = 4,
        [3] = 5
    };
}
var soldier = new Soldier { Level = 2 };
Console.WriteLine(soldier.MaxHp); // 6

public class Unit
    {
        public static Dictionary<string, Unit> units { get; set; } // string is unit name

        public string name { get; set; }
        public int level { get; set; }
        public Dictionary<string, int> attributes { get; set; }
    }

    public struct AttributeKey : IEquatable<AttributeKey>
{
    public AttributeKey(Class @class, Attribute attribute, int level)
    {
        Class = @class;
        Attribute = attribute;
        Level = level;
    }

    public readonly Class Class;
    public readonly Attribute Attribute;
    public readonly int Level;

    public bool Equals(AttributeKey other)
    {
        return Class == other.Class && Attribute == other.Attribute && Level == other.Level;
    }
    public override bool Equals(object obj)
    {
        return obj is AttributeKey && Equals((AttributeKey)obj);
    }
    public override int GetHashCode()
    {
        unchecked
        {
            return (((Class.GetHashCode() * 397) ^ Attribute.GetHashCode()) * 397) ^ Level;
        }
    }
}

private static readonly CharLookup _lookup = new CharLookup();

private static void Main()
{
   _lookup[CharacterClass.Mage, CharacterTrait.SingingAbility, 2] = 123;
   _lookup[CharacterClass.Mage, CharacterTrait.SingingAbility, 3] = 234;
   _lookup[CharacterClass.Soilder, CharacterTrait.MaxBeers, 3] = 23423;

   Console.WriteLine("Mage,SingingAbility,2 = " + _lookup[CharacterClass.Mage, CharacterTrait.SingingAbility, 2]);
   Console.WriteLine("Soilder,MaxBeers,3 = " + _lookup[CharacterClass.Soilder, CharacterTrait.MaxBeers, 3]);
}

public enum CharacterClass
{
   Soilder,

   Mage,

   SmellyCoder
}

public enum CharacterTrait
{
   MaxHp,

   MaxBeers,

   SingingAbility
}
public class CharLookup
{
   private Dictionary<Tuple<CharacterClass, CharacterTrait, int>, int> myDict = new Dictionary<Tuple<CharacterClass, CharacterTrait, int>, int>();

   public int this[CharacterClass characterClass, CharacterTrait characterTrait, int level]
   {
      get => Check(characterClass, characterTrait, level);
      set => Add(characterClass, characterTrait, level, value);
   }

   public void Add(CharacterClass characterClass, CharacterTrait characterTrait, int level, int value)
   {
      var key = new Tuple<CharacterClass, CharacterTrait, int>(characterClass, characterTrait, level);

      if (myDict.ContainsKey(key))
         myDict[key] = value;
      else
         myDict.Add(key, value);
   }

   public int Check(CharacterClass characterClass, CharacterTrait characterTrait, int level)
   {
      var key = new Tuple<CharacterClass, CharacterTrait, int>(characterClass, characterTrait, level);

      if (myDict.TryGetValue(key, out var result))
         return result;

      throw new ArgumentOutOfRangeException("blah");
   }
}

public enum UnitType
{
    Soldier,
    Mage
}

public enum StatType
{
    MaxHP,
    MaxMP,
    Attack,
    Defense
}

// Where the unit initialisation data is stored
public static class UnitData
{
    private static Dictionary<string, Dictionary<StatType, int>> Data = new Dictionary<UnitType, Dictionary<StatType, int>>();

    private static string GetKey(UnitType unitType, int level)
    {
        return $"{unitType}:{level}";
    }

    public static AddUnit(UnitType unitType, int level, int maxHP, int maxMP, int attack, int defense)
    {
        Data.Add(GetKey(unitType, level), 
            new Dictionary<StatType, int> 
            {
                { StatType.MaxHP, maxHP },
                { StatType.MaxMP, maxMP },
                { StatType.Attack, attack },
                { StatType.Defense, defense }
            });
    }

    public static int GetStat(UnitType unitType, int level, StatType statType)
    {
        return Data[GetKet(unitType, level][statType];
    }
}

// The data is not stored against the unit but referenced from UnitData
public class Unit
{
    public UnitType UnitType { get; private set; }
    public int Level { get; private set; }
    public Unit(UnitType unitType, int level)
    {
        UnitType = unitTypel
        Level = level;
    }
    public int GetStat(StatType statType)
    {
        return UnitData.GetStat(UnitType, Level, statType);
    }
}

// To initialise the data
public class StartClass
{
    public void InitialiseData()
    {
        UnitData.Add(UnitType.Soldier, 1, 5, 0, 1, 1);
        UnitData.Add(UnitType.Soldier, 2, 6, 0, 2, 2);
        UnitData.Add(UnitType.Soldier, 3, 8, 0, 3, 3);

        UnitData.Add(UnitType.Mage, 1, 3, 10, 1, 1);
        UnitData.Add(UnitType.Mage, 2, 4, 15, 2, 2);
        UnitData.Add(UnitType.Mage, 3, 5, 20, 3, 3);
    }
}

// Use of units
public class Level1
{
    public List<Unit> Units = new List<Unit>();
    public void InitialiseUnits()
    {
        Units.Add(new Unit(UnitType.Soldier, 1));
        Units.Add(new Unit(UnitType.Soldier, 1));
        Units.Add(new Unit(UnitType.Mage, 1));
        Units.Add(new Unit(UnitType.Mage, 1));
    }
    public void Something()
    {
        int maxHP = Units.First().GetStat(StatType.MaxHP);
        // etc
    }
}