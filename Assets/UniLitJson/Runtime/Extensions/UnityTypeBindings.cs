using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UniLitJson
{
    public static class UnityTypeBindings
    {
        private static bool hasRegistered;

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
#else
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#endif
        public static void Register()
        {
            if (hasRegistered)
                return;

            hasRegistered = true;

            JsonMapper.RegisterExporter<Type>((value, writer) => writer.Write(value.FullName));
            JsonMapper.RegisterImporter<string, Type>(Type.GetType);

            JsonMapper.RegisterExporter<Vector2>(WriteVector2);
            JsonMapper.RegisterExporter<Vector2Int>(WriteVector2Int);
            JsonMapper.RegisterExporter<Vector3>(WriteVector3);
            JsonMapper.RegisterExporter<Vector3Int>(WriteVector3Int);
            JsonMapper.RegisterExporter<Vector4>(WriteVector4);
            JsonMapper.RegisterExporter<Quaternion>(WriteQuaternion);
            JsonMapper.RegisterExporter<Rect>(WriteRect);
            JsonMapper.RegisterExporter<RectInt>(WriteRectInt);
            JsonMapper.RegisterExporter<RectOffset>(WriteRectOffset);
            JsonMapper.RegisterExporter<RangeInt>(WriteRangeInt);
            JsonMapper.RegisterExporter<Bounds>(WriteBounds);
            JsonMapper.RegisterExporter<BoundsInt>(WriteBoundsInt);
            JsonMapper.RegisterExporter<Color>(WriteColor);
            JsonMapper.RegisterExporter<Color32>(WriteColor32);
        }

        private static void WriteVector2(Vector2 value, JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WriteProperty("x", value.x);
            writer.WriteProperty("y", value.y);
            writer.WriteObjectEnd();
        }

        private static void WriteVector2Int(Vector2Int value, JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WriteProperty("x", value.x);
            writer.WriteProperty("y", value.y);
            writer.WriteObjectEnd();
        }

        private static void WriteVector3(Vector3 value, JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WriteProperty("x", value.x);
            writer.WriteProperty("y", value.y);
            writer.WriteProperty("z", value.z);
            writer.WriteObjectEnd();
        }

        private static void WriteVector3Int(Vector3Int value, JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WriteProperty("x", value.x);
            writer.WriteProperty("y", value.y);
            writer.WriteProperty("z", value.z);
            writer.WriteObjectEnd();
        }

        private static void WriteVector4(Vector4 value, JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WriteProperty("x", value.x);
            writer.WriteProperty("y", value.y);
            writer.WriteProperty("z", value.z);
            writer.WriteProperty("w", value.w);
            writer.WriteObjectEnd();
        }

        private static void WriteQuaternion(Quaternion value, JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WriteProperty("x", value.x);
            writer.WriteProperty("y", value.y);
            writer.WriteProperty("z", value.z);
            writer.WriteProperty("w", value.w);
            writer.WriteObjectEnd();
        }

        private static void WriteRect(Rect value, JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WriteProperty("x", value.x);
            writer.WriteProperty("y", value.y);
            writer.WriteProperty("width", value.width);
            writer.WriteProperty("height", value.height);
            writer.WriteObjectEnd();
        }

        private static void WriteRectInt(RectInt value, JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WriteProperty("x", value.x);
            writer.WriteProperty("y", value.y);
            writer.WriteProperty("width", value.width);
            writer.WriteProperty("height", value.height);
            writer.WriteObjectEnd();
        }

        private static void WriteRectOffset(RectOffset value, JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WriteProperty("top", value.top);
            writer.WriteProperty("left", value.left);
            writer.WriteProperty("bottom", value.bottom);
            writer.WriteProperty("right", value.right);
            writer.WriteObjectEnd();
        }

        private static void WriteRangeInt(RangeInt value, JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WriteProperty("start", value.start);
            writer.WriteProperty("length", value.length);
            writer.WriteObjectEnd();
        }

        private static void WriteBounds(Bounds value, JsonWriter writer)
        {
            writer.WriteObjectStart();

            writer.WritePropertyName("center");
            WriteVector3(value.center, writer);

            writer.WritePropertyName("size");
            WriteVector3(value.size, writer);

            writer.WriteObjectEnd();
        }

        private static void WriteBoundsInt(BoundsInt value, JsonWriter writer)
        {
            writer.WriteObjectStart();

            writer.WritePropertyName("center");
            WriteVector3(value.center, writer);

            writer.WritePropertyName("size");
            WriteVector3(value.size, writer);

            writer.WriteObjectEnd();
        }

        private static void WriteColor(Color value, JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WriteProperty("r", value.r);
            writer.WriteProperty("g", value.g);
            writer.WriteProperty("b", value.b);
            writer.WriteProperty("a", value.a);
            writer.WriteObjectEnd();
        }

        private static void WriteColor32(Color32 value, JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WriteProperty("r", value.r);
            writer.WriteProperty("g", value.g);
            writer.WriteProperty("b", value.b);
            writer.WriteProperty("a", value.a);
            writer.WriteObjectEnd();
        }
    }
}