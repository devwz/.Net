﻿using DefaultProject.Common.Data.Interfaces;
using DefaultProject.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultProject.Common.Data
{
    public class CharacterDependency : ICharacterDependency
    {
        public CharacterDependency()
        {

        }

        static Random random = new Random();

        public Character CreateCharacter()
        {
            return new Character()
            {
                Title = CreateTitle(),
                Health = CreateHealth()
            };
        }

        public ICollection<Character> CreateCharacterCollection(int length)
        {
            Character[] collection = new Character[length];

            for (int i = 0; i < length; i++)
            {
                collection[i] = CreateCharacter();
            }

            return collection;
        }

        string CreateTitle()
        {
            string[] collection = new string[] { "Ariana", "Autumn", "Bartle", "Bohn", "Catherine", "Cloud", "Dalila", "Delta", "Diana", "Elit", "Emeline", "Finn", "Franklin", "Gorge", "Indigo", "Itham", "Libero", "Lillian", "Orion", "Ozotl", "Noel", "Rod", "Shel", "Stone", "Tiena", "Vivian", "Zagno" };
            return collection[random.Next(0, collection.Length)];
        }

        int CreateHealth()
        {
            return random.Next(60, 180);
        }
    }
}
