using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using sfservice.API.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace sfservice.APITests.Services
{
    internal class ConfigurationTest : IConfiguration
    {
        private Dictionary<string, string> dictionary = new Dictionary<string, string> {
            { "CSVFilesLocation", "" }
        };

        public string this[string key] {
            get {
                return dictionary[key];
            }
            set {
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key] = value;
                }
                else
                {
                    dictionary.Add(key, value);
                }
            }
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }
    }

    public class DungeonServiceTest
    {
        [Fact]
        public void GetDungeons_ReturnsListOfDungeons()
        {
            var config = new ConfigurationTest();
            var s = Directory.GetCurrentDirectory();
            var sArray = s.Split('\\');
            string dir = "";

            foreach(var n in sArray)
            {
                dir += $"{n}\\";
                if (n == "sf-service") break;
            }


            config["CSVFilesLocation"] = dir;

            var service = new DungeonService(config);
            var results = service.GetDungeons();

            Assert.NotNull(results);
            Assert.True(results.Count > 0);
        }
    }
}
