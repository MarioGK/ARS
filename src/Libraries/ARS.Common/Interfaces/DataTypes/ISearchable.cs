using System.Runtime.Serialization;

namespace ARS.Common.Interfaces.DataTypes;

public interface ISearchable
{
    [IgnoreDataMember]
    string SearchString { get; }
}