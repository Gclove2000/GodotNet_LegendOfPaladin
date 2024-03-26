using Bogus;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotProgram.Interfaces
{
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class T_ModelBase
    {
        /// <summary>
        /// 主键自增
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 假删除
        /// </summary>
        public bool IsDelete { get; set; } = false;

        public DateTime DeleteTime { get; set; }
    }
}
