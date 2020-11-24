using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sfservice.API.Services
{
    public interface ICsvService
    {
        List<Type> ReadRecordsFromCSVFile<Type, TypeMapper>(string location) where TypeMapper : ClassMap<Type>;
    }
}
