using System;
using System.Collections.Generic;
using Entity.@base;

namespace Entity
{
    [Serializable]
    public class ResourceDetailEntity:BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public SpareResourceTypeEntity Type { get; set; }

        /// <summary>
        /// 文件地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 文件简介
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 上传者
        /// </summary>
        public string UploadPeople { get; set; }

        public List<ResourceDetailUploadEntity>  Upload { get; set; }
        public ResourceDetailEntity()
        {

        }
    }
}
