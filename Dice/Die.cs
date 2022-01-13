using System;

namespace Dice
{
    public class Die
    {
        private static readonly Random Random = new Random();
        public readonly int SideCount;
        public int Value { get; private set; }

        public Die(int sides = 6)
        {
            SideCount = sides < 2 ? throw new ArgumentOutOfRangeException(nameof(sides),"You must have at least 2 sides on your die") : sides;
            Roll();
        }
        
        public void Roll()
        {
            Value = Random.Next(1, SideCount + 1);
        }
        
        public override string ToString()
        { 
            return $"{Value}";
        }
        
        // Note: We don't need a GetValue method because we can just use the property
    }
}