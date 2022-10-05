using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Dtos.FIght
{
    public class HighsocreDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}