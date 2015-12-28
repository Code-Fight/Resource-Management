using Entity.@base;

namespace Entity
{
    /// <summary>
    /// 资源上传文件实体
    /// </summary>
    public class ResourceDetailUploadEntity : BaseEntity
    {
        public  int Id { get; set; }
        public int Resource_detail_id { get; set; }
        public string Files_name { get; set; }
        public string FilesDir { get; set; }
        public ResourceDetailUploadEntity ()
        {
            
        }
    }
}
