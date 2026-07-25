using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BloodBank.Models.Attributes;

namespace BloodBank.EF.Tables;

public partial class Donor
{
    public int DonorId { get; set; }

    public string FullName { get; set; } = null!;

    public string BloodGroup { get; set; } = null!;

    [StringLength(11, MinimumLength = 11, ErrorMessage = "Contact number must be exactly 11 characters long")]
    public string ContactNo { get; set; } = null!;

    public string City { get; set; } = null!;

    [PastDate(ErrorMessage = "The last donation date must be a past date.")]
    public DateOnly LastDonationDate { get; set; }

    public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
}
