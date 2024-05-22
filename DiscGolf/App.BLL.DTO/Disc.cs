using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class Disc : IDomainEntityId
{
    public Guid Id { get; set; }
    
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    public double Speed { get; set; }
    public double Glide { get; set; }
    public double Turn { get; set; }
    public double Fade { get; set; }
    
    public Guid ManufacturerId { get; set; }
    public string ManufacturerName { get; set; } = default!;
    public Manufacturer? Manufacturers { get; set; }

    public string CategoryName { get; set; } = default!;
    public Guid CategoryId { get; set; } 
    public Category? Categories { get; set; }
}