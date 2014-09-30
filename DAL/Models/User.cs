﻿using System;
using System.ComponentModel.DataAnnotations;
using DAL.Models.Contacts;

namespace DAL.Models
{
    public class User
    {
        [Required]
        [Key]
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        public bool Sex { get; set; }

        [Required]
        public bool Married { get; set; }

        public int Sallary { get; set; }

        public PrimaryContact PrimaryContact { get; set; }

        public SecondaryContact SecondaryContact { get; set; }

        public AdministrativeContact AdministrativeContact { get; set; }
    }
}