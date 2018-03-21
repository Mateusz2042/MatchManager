using MatchManager.Enums;
using MatchManager.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Tests.TestDataGenerator
{
    public class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>()
        {
            new object[]
            {
                new Player{FirstName = "", LastName = "Nowak", NickName = "Mat", Age = 23, Sex = Sex.Male, IsDeleted = false},
                false
            },
            new object[]
            {
                new Player{FirstName = "Kasia", LastName = "", NickName = "Kao", Age = 22, Sex = Sex.Female, IsDeleted = false},
                false
            },
            new object[]
            {
                new Player{FirstName = "Jan", LastName = "Nowy", NickName = "", Age = 20, Sex = Sex.Male, IsDeleted = false},
                false
            },
            new object[]
            {
                new Player{FirstName = "Marcin", LastName = "Wojtowski", NickName = "MR", Age = 10, Sex = Sex.Male, IsDeleted = false},
                false
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
