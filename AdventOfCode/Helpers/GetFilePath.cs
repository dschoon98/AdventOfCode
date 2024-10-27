using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Helpers
{
    public class GetFilePath
    {
        private readonly string _fileName;
        private readonly string _day;
        private readonly string _year;
        public GetFilePath(string fileName,string day, string year) { 
            _fileName = fileName;
            _day = day;
            _year = year;
        }
        public string GetPath()
        {
            string assemblyPath = Directory.GetCurrentDirectory();
            string projectPath = Path.GetFullPath(Path.Combine(assemblyPath, @"..\..\..\"));
            return Path.Combine(projectPath, $"{_year}", $"Day {_day}", _fileName);
        }
    }
}
