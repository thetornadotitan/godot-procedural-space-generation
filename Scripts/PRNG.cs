using Godot;
using System;

namespace t3
{
    public static class PRNG
    {
        private static readonly RandomNumberGenerator rng = new();

        /// <summary>
        /// Seeds Godot's prng with the given number.
        /// </summary>
        /// <param name="seed">The number with which to seed Godot's prng.</param>
        public static void Seed(ulong seed)
        {
            rng.Seed = seed;
        }

        /// <summary>
        /// Seeds Godot's prng with the hash code of a given string.
        /// </summary>
        /// <param name="seed">The string with which to hash for Godot's prng.</param>
        public static void Seed(string seed)
        {
            rng.Seed = (ulong)seed.GetHashCode();
        }

        /// <summary>
        /// Returns integer between min and max inclusive
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int NextInt(int min = 0, int max = int.MaxValue)
        {
            return rng.RandiRange(min, max);
        }

        /// <summary>
        /// Returns float between min and max inclusive
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float NextFloat(float min = 0, float max = float.MaxValue)
        {
            return rng.RandfRange(min, max);
        }
    }
}
