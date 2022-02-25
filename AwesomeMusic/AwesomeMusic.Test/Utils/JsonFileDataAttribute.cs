namespace AwesomeMusic.Test.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Newtonsoft.Json;
    using Xunit.Sdk;

    public class JsonFileDataAttribute : DataAttribute
    {
        private readonly string _filePath;
        private readonly Type _dataType;

        public JsonFileDataAttribute(string filePath, Type dataType)
        {
            _filePath = $"Data/{filePath}";
            _dataType = dataType;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null) { throw new ArgumentNullException(nameof(testMethod)); }

            // Get the absolute path to the JSON file
            var path = Path.IsPathRooted(_filePath)
                ? _filePath
                : Path.GetRelativePath(Directory.GetCurrentDirectory(), _filePath);

            if (!File.Exists(path))
            {
                throw new ArgumentException($"Could not find file at path: {path}");
            }

            // Load the file
            var fileData = File.ReadAllText(_filePath);

            var specific = typeof(List<>).MakeGenericType(_dataType);

            dynamic datalist = JsonConvert.DeserializeObject(fileData, specific);

            var objectList = new List<object[]> { datalist.ToArray() };

            return objectList;
        }
    }
}
