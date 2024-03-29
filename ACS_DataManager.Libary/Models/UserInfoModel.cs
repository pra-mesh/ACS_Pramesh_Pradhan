﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS_DataManager.Library.Models;
public class UserInfoModel
{
    [Required, MaxLength(256, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
    public string username { get; set; } = "";
    [Required]
    public string Id { get; set; } = "";
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; } = "";

    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "{0} value must be number")]
    public string PhoneNumber { get; set; } = "";
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";
}
