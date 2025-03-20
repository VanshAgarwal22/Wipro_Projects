﻿using System.ComponentModel.DataAnnotations;

namespace Customer_Project.Models
{
    public class Customer
    {
        [Key]
        public int CustId { get; set; }
        public string? CustName { get; set; }
        public string? CustUserName { get; set; }
        public string? CustPassword { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
    }
}
