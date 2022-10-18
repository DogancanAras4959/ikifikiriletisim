using ikifikir.COMMON.DataTransfer.PostData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.ENGINES.Interface
{
    public interface IPostService
    {
        Task<bool> insertPost(PostDto model);
        Task<bool> updatePost(PostDto model);
        bool deletePost(int id);
        Task<bool> postIsActiveProcess(int id);
        Task<bool> postNotificationEdit(int id);
        PostDto getPostById(int id);
        List<PostListItemDto> getPostList();
        List<PostListItemDto> getPostListByNotification();
        List<PostListItemDto> getPostTakeToLast();
        List<PostListItemDto> searchDataInPost(string searchKey);
    }
}
