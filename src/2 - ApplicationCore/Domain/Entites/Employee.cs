using System;
using System.Collections.Generic;
using Domain.ValueObjects;

namespace Domain.Entites
{
    public class Employee
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public Dictionary<string, Award> Awards { get; private set; }

        public Employee(string id, string name)
        {
            Id = id;
            Name = name;
            Awards = new Dictionary<string, Award>();
        }

        public void VestingAward(string awardId, decimal quantity, int round)
        {
            Award current;
            if (Awards.TryGetValue(awardId, out current))
            {
                current.Add(quantity,round);
                Awards[awardId] = current;
            }
            else { Awards.Add(awardId, new Award(awardId, quantity)); }

        }

        public void VestingCancel(string awardId, decimal quantity, int round)
        {
            Award current;
            if (Awards.TryGetValue(awardId, out current))
            {
                current.Remove(quantity,round);
                Awards[awardId] = current;
            }
            else { Awards.Add(awardId, new Award(awardId, decimal.Zero)); }

        }
    }
}
