using System;

namespace UniLitJson
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class JsonIgnore : Attribute
    {
    }
}