using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkNet.Domain.Core;
using ThinkCRM.Infrastructure.DataEntity.Co;

namespace ThinkCRM.Domain.Co
{
    public class Individualize : AggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public CM_Individualize MCM_Individualize { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public List<CM_IndividualizeLine> ListCM_IndividualizeLine { get; private set; }

        public string RelationPosition { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataEntity"></param>
        public Individualize(CM_Individualize dataEntity, List<CM_IndividualizeLine> listCM_IndividualizeLine, string relationPosition)
        {
            this.MCM_Individualize = dataEntity;
            this.ListCM_IndividualizeLine = listCM_IndividualizeLine;
            this.RelationPosition = relationPosition;
        }
    }
}
