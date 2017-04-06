using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Component
{
    public interface IJsonSerializer
    {
        string Serialize(object obj);
        object Deserialize(string value, Type type);
        T Deserialize<T>(string value) where T : class;

        string SerializeDataTable(DataTable dt);
        string SerializeEnumerable4EasyuiGrid(IEnumerable objects, int totalCount);
        string SerializeDataTable4EasyuiGrid(DataTable dt, int totalCount);
    }
}
