using System;
using Entity.@base;

namespace Entity
{
    [Serializable]
    public class SpareResourceTypeEntity : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 目录
        /// </summary>
        public string Directory { get; set; }

        public SpareResourceTypeEntity()
        {

        }
    }
}
