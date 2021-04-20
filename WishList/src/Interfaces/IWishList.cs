using System.Threading.Tasks;
using System.Collections.Generic;

namespace WishList.Interfaces
{
    public interface IWishList
    {
        Task<WishListItem> AddOrRefreshAsync(string itemName);
        Task<IEnumerable<WishListItem>> AllAsync();
    }
}
