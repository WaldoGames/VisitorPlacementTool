﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorPlaceMentToolEnums;

namespace VisitorPlaceMentToolInterfaces
{
    public interface IPerson
    {
        public string Name { get; set; }

        public bool Child { get;}

        public bool Placed { get; set; }

        public string Id { get; }

        public bool Cancelled { get; set; }

        public IGroup Parent { get; set; }

        public IChair? Chair { get; set; }

        public PersonState PersonState { get; set; }

        public DateTime BirthDay { get; set; }

        public void SetParent(IGroup parentGroup);
    }
}
