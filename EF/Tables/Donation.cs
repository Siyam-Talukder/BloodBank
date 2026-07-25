using System;
using System.Collections.Generic;
using BloodBank.Models.Attributes;

namespace BloodBank.EF.Tables;

public partial class Donation
{
    public int DonationId { get; set; }

    public int DonorId { get; set; }

    [PastDate(ErrorMessage = "The donation date must be a past date.")]
    public DateOnly DonationDate { get; set; }

    public int VolumeMl { get; set; }

    public string CampName { get; set; } = null!;

    public virtual Donor Donor { get; set; } = null!;
}
