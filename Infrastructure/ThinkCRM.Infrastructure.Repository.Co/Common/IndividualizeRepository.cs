using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkCRM.Domain.Co;
using ThinkCRM.Infrastructure.DataEntity.Co;
using ThinkNet.Domain.Core;
using ThinkNet.Persistence.Core;

namespace ThinkCRM.Infrastructure.Repository.Co
{
    public class IndividualizeRepository : SQLRepositoryBase<Individualize>, IIndividualizeRepository
    {
        #region 字    段

        private IDAO<CM_Individualize> _mDAO = null;
        private IDAO<CM_IndividualizeLine> _mDaoLine = null;

        #endregion

        #region 属    性

        #endregion

        #region 构造函数
        public IndividualizeRepository(IDAOCenter daoCenter)
            : this(null, daoCenter)
        {
        }

        public IndividualizeRepository(IRepositoryContext context, IDAOCenter daoCenter)
            : base(context, daoCenter)
        {
            
        }
        #endregion

        #region 基本方法
        protected override void PersistAdd(Individualize aggregateRoot)
        {
            int individualizeId = _mDAO.Add(aggregateRoot.MCM_Individualize);
            
            if (aggregateRoot.ListCM_IndividualizeLine != null && aggregateRoot.ListCM_IndividualizeLine.Count > 0)
            {
                foreach (CM_IndividualizeLine item in aggregateRoot.ListCM_IndividualizeLine)
                {
                    item.IndividualizeID = individualizeId;
                    _mDaoLine.Add(item);
                }
            }
        }

        protected override void PersistUpdate(Individualize aggregateRoot)
        {
            _mDAO.Update(aggregateRoot.MCM_Individualize);

            string where = string.Format("IndividualizeID={0} AND RelationPosition=N'{1}'", aggregateRoot.MCM_Individualize.ID,aggregateRoot.RelationPosition);
            _mDaoLine.Delete(where);
            if (aggregateRoot.ListCM_IndividualizeLine != null && aggregateRoot.ListCM_IndividualizeLine.Count > 0)
            {
                foreach (CM_IndividualizeLine item in aggregateRoot.ListCM_IndividualizeLine)
                {
                    item.IndividualizeID = aggregateRoot.MCM_Individualize.ID;
                    _mDaoLine.Add(item);
                }
            }
        }

        protected override void PersistDelete(Individualize aggregateRoot)
        {
            _mDAO.Delete(aggregateRoot.MCM_Individualize.ID);

            if (aggregateRoot.ListCM_IndividualizeLine != null && aggregateRoot.ListCM_IndividualizeLine.Count > 0)
            {
                foreach (CM_IndividualizeLine item in aggregateRoot.ListCM_IndividualizeLine)
                {
                    _mDaoLine.Delete(item.ID);
                }
            }
        }
        #endregion

        #region 其他方法
        protected override void CreateClusteDAO()
        {
            _mDAO = CreateDAO<CM_Individualize>();
            _mDaoLine = CreateDAO<CM_IndividualizeLine>();
        }
        #endregion
    }
}
