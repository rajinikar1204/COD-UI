using ADP.TLM.Framework.DAL.DataAccess.Context;
using ADP.TLM.Framework.DTO;
using System.Diagnostics.CodeAnalysis;

namespace ADP.TaaS.COD
{
    public interface ISetContext
    {
        void SetDataContext(string orgoId);
    }
    [ExcludeFromCodeCoverage]
    public class SetContext:ISetContext
    {
        public void SetDataContext(string orgoId)
        {
            if (!string.IsNullOrEmpty(orgoId))
                DataAccessContext.Instance().SetClientContext(orgoId, "ORGOID", new ApplicationKeys(), null, null, "");
        }
    }
}