using Base.Contracts.Domain;

namespace App.Domain;

public class DiscsInWishlist : IDomainEntityId
{
    public Guid Id { get; set; }
    
    //public Guid DiscId { get; set; }
    //public Disc? Discs { get; set; }
    
    public Guid DiscFromPageId { get; set; }
    public DiscFromPage? DiscFromPage { get; set; }
    
    public Guid WishlistId { get; set; }
    public Wishlist? Wishlists { get; set; }
    
}