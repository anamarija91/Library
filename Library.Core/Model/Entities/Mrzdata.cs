﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace Library.Core.Model.Entities
{
    public partial class Mrzdata
    {
        public Mrzdata()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string FirstRow { get; set; }
        public string SecondRow { get; set; }
        public string ThirdRow { get; set; }
        public bool? Dobvalid { get; set; }
        public bool? CardNumberValid { get; set; }
        public bool? Doevalid { get; set; }
        public bool? CompositeCheckValid { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}