﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorPlaceMentToolEnums;
using VisitorPlaceMentToolInterfaces;

namespace VisitorPlacementToolController.Objects
{
    public class Person : IPerson
    {
        public Person(IGroup parentgroup) { SetParent(parentgroup); }

        public bool Child { get {
                DateTime ZeroTime= new DateTime(1,1,1);
                TimeSpan span = (DateTime.Now - BirthDay);
                if((ZeroTime+span).Year-1 < 12)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            
            } }

        private bool placed;
        public bool Placed { get { return placed; } set { placed = value; } }
        
        public bool cancelled;
        public bool Cancelled { get { return cancelled; } set { cancelled = value; } }

        private IChair? chair;

        public IChair? Chair
        {
            get { return chair; }
            set { chair = value; }
        }

        public string Id { get { return parent.Id; } }

        public IGroup parent;
        public IGroup Parent { get { return parent; } set { parent = value; } }

        public PersonState personState;
        public PersonState PersonState { get { return personState; } set { personState = value; } }

        public string name { get; set; }
        public string Name { get { return name; } set { name = value; } }
        private DateTime birthday;

        public DateTime BirthDay
        {
            get { return birthday; }
            set { birthday = value; }
        }

        public void SetParent(IGroup parentGroup)
        {
            Parent = parentGroup;
        }
    }
}
