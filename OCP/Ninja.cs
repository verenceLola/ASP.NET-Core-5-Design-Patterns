using System;

namespace OCP
{
    public interface IAttackable { }
    public class Ninja : IAttackable
    {
        public string Name { get; }
        public Weapon EquippedWeapon { get; set; }
        public Ninja(string name)
        {
            Name = name;
        }
        public AttackResult Attack(IAttackable target) => new AttackResult(EquippedWeapon, this, target);
        public override string ToString() => Name;
    }
    public class Weapon { }
    public class Sword : Weapon { }
    public class Shuriken : Weapon { }
    public class AttackResult
    {
        public Weapon Weapon { get; }
        public IAttackable Attacker { get; }
        public IAttackable Target { get; }
        public AttackResult(Weapon weapon, IAttackable attacker, IAttackable target)
        {
            Weapon = weapon;
            Attacker = attacker;
            Target = target;
        }
    }
}
