using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceBookClient.Models.ModelsForLiveInfo
{
    public interface IAllPostInfo
    {
        string Discription { get; set; }

        DateTime DatePost { get; set; }

        string UserId { get; set; }

        IEnumerable<IAllPostInfo> GetDataAllPost();
    }
}
