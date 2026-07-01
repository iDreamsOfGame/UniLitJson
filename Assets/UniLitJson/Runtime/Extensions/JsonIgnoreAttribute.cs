using System;

namespace LitJson
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class JsonIgnore : Attribute
    {
    }
}