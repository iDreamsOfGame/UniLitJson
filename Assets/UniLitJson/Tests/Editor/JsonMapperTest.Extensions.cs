using NUnit.Framework;

namespace LitJson.Tests
{
    internal class JsonIgnoreFieldObject
    {
        [JsonIgnore]
        public int ignoreFiled;
    }

    internal class JsonIgnorePropertyObject
    {
        [JsonIgnore]
        public int IgnoreProperty { get; set; }
    }
    
    public partial class JsonMapperTest
    {
        [Test]
        public void JsonIgnoreFieldTest()
        {
            var jsonData = JsonMapper.ToJson(new JsonIgnoreFieldObject());
            Assert.AreEqual("{}", jsonData);
        }

        [Test]
        public void JsonIgnorePropertyTest()
        {
            var jsonData = JsonMapper.ToJson(new JsonIgnorePropertyObject());
            Assert.AreEqual("{}", jsonData);
        }
    }
}