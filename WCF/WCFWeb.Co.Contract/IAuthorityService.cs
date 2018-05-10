using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using ThinkNet.Infrastructure.Core;

namespace WCFWeb.Co.Contract
{
    [ServiceContract]
    public interface IAuthorityService
    {
       
        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetCallbackCode(string code, string state);
    }

}