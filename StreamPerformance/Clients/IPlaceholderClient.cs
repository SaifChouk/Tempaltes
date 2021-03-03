using System.Collections.Generic;
using System.Threading.Tasks;

namespace StreamPerformance
{
    public interface IPlaceholderClient
    {
        #region Using as readasStringasync
        Task<List<Album>> GetAlbums();
        Task<List<Comment>> GetComments();
        Task<List<Photo>> GetPhotos();
        Task<List<Post>> GetPost();
        #endregion

        #region Using HttpCompletionOption.ResponseHeadersRead and stream serialisation
        Task<List<Album>> GetAlbumsAsStream();
        Task<List<Comment>> GetCommentsAsStream();
        Task<List<Photo>> GetPhotosAsStream();
        Task<List<Post>> GetPostAsStream();
        #endregion

        #region Using Simplified Stream serialisation
        Task<List<Album>> GetAlbumsAsStreamSimplified();
        Task<List<Comment>> GetCommentsAsStreamSimplified();
        Task<List<Photo>> GetPhotosAsStreamSimplified();
        Task<List<Post>> GetPostAsStreamSimplified();
        #endregion
    }
}